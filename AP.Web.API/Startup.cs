using AP.Web.Common.Attributes;
using AP.Web.Common.Authentication;
using AP.Web.Common.Constants;
using AP.Web.Common.Extensions;
using AP.Web.Persistence.Data;
using AP.Web.Persistence.Data.Entities;
using AP.Web.Persistence.Repository;
using AP.Web.Persistence.UnitOfWork;
using AP.Web.Services.Models;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;

namespace WebApiAuth
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString(KeyConstants.DefaultConnection),
              b => b.MigrationsAssembly(KeyConstants.MigrationsAssemblyName))).AddUnitOfWork<ApplicationDbContext>();

      services.AddScoped<DbContext, ApplicationDbContext>();
      services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
      services.AddScoped<IDataContext, DataContext>();
      services.AddScoped<ITransactionScope, TransactionScope>();

      //services.AddScoped<IUnitOfWorkProvider, UnitOfWorkProvider>(); 

      services.AddSingleton<IJwtFactory, JwtFactory>();

      // Register the ConfigurationBuilder instance of FacebookAuthSettings
      services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));
      services.AddCors(options =>
      {
        options.AddPolicy(KeyConstants.MyAllowSpecificOrigins,
        builders =>
        {
          builders.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
      });

      services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

      // jwt wire up
      // Get options from app settings
      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

      // Configure JwtIssuerOptions
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(KeyConstants.SigningKey, SecurityAlgorithms.HmacSha256);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = KeyConstants.SigningKey,

        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(configureOptions =>
      {
        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        configureOptions.TokenValidationParameters = tokenValidationParameters;
        configureOptions.SaveToken = true;
      });

      // api user claim policy
      services.AddAuthorization(options =>
      {
        options.AddPolicy(KeyConstants.PolicyAuthoApiUser, policy => policy.RequireClaim(KeyConstants.Strings.JwtClaimIdentifiers.Rol,
          KeyConstants.Strings.JwtClaims.ApiAccess));
      });

      // add identity
      var builder = services.AddIdentityCore<AppUser>(o =>
      {
        // configure identity options
        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
      });

      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
      builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

      //TODO: Generic TEntities

      services.AddAutoMapper();
      services.AddLogging();
      services.AddMvc(opts =>
      {
        opts.Filters.Add(new AutoAttribute());
      }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
      //services.AddSingleton<IDataContext>(); // AddScoped<IDataContext, DataContext>();
      services.AutoRegisterDI();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseExceptionHandler(
          builder =>
          {
            builder.Run(async context =>
             {
               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               context.Response.Headers.Add(KeyConstants.AccessControl, "*");

               var error = context.Features.Get<IExceptionHandlerFeature>();
               if (error != null)
               {
                 context.Response.AddApplicationError(error.Error.Message);
                 await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
               }
             });
          });

      app.Use(async (context, next) =>
      {
        await next();

        if (!context.Response.Headers.ContainsKey(KeyConstants.AccessControl))
        {
          context.Response.Headers.Add(KeyConstants.AccessControl, "*");
        }

        //Response header manipulation logic
      });

      app.UseAuthentication();
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseCors(KeyConstants.MyAllowSpecificOrigins);
      app.UseMvc();
    }
  }
}
