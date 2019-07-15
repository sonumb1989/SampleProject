using AP.Web.Common.Constants;
using AP.Web.Common.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AP.Web.Common.Extensions
{
    public static class StartupExtensions
  {
    public static IServiceCollection Service;

    /// <summary>
    /// AutoRegisterDI
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="services">services</param>
    /// <returns></returns>
    public static IServiceCollection AutoRegisterDI(this IServiceCollection services)
    {
      Service = services;
      var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString());
      var assemblys = assemblyNames
                  .Select(Assembly.Load)
                  .SelectMany(a => a.ExportedTypes)
                  .Where(x => x.IsClass && x.Namespace != null
                     && (
                          (x.Namespace.StartsWith(KeyConstants.AssemblyServices, StringComparison.InvariantCultureIgnoreCase))
                          || (x.Namespace.StartsWith(KeyConstants.AssemblyAction, StringComparison.InvariantCultureIgnoreCase))
                      ));

      var dicTypeMapiing = new Dictionary<Type, HashSet<Type>>();

      foreach (var assemType in assemblys)
      {
        assemType.GetInterfaces()
             .Select(i => i.IsGenericType ? i.GetGenericTypeDefinition() : i)
             .Where(i => i.Name.Contains(assemType.Name)).ToList()
             .ForEach(iOfType =>
             {
               if (!dicTypeMapiing.ContainsKey(iOfType))
               {
                 dicTypeMapiing[iOfType] = new HashSet<Type>();
               }

               dicTypeMapiing[iOfType].Add(assemType);
             });
      }

      foreach (var typeMap in dicTypeMapiing)
      {
        if (typeMap.Value.Count == 1)
        {
          services.AddScoped(typeMap.Key, typeMap.Value.First());
        }
      }

      return services;
    }

    /// <summary>
    /// Configure
    /// </summary>
    public static void Configure() { }

    /// <summary>
    /// Resolve
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <returns>T</returns>
    public static T Resolve<T>()
    {
      T ret = default(T);

      try
      {
        ret = (T)Service.BuildServiceProvider().GetService(typeof(T));
      }
      catch (Exception ex)
      {
        throw new Exception($"Can't Resolve :{ExceptionUtilities.GetFullExceptionMessage(ex)}");
      }

      if (ret == null)
      {
        throw new InvalidOperationException(string.Format("Type {0} not registered in service", typeof(T).Name));
      }

      return ret;
    }
  }
}
