using AP.Web.Persistence.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AP.Web.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        /// <summary>
        /// Custom table name : TableNameEntities ==> TableName
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Model.GetEntityTypes().ToList().ForEach(entity =>
            {
                string tableName = entity.Relational().TableName;
                entity.Relational().TableName = tableName.Contains("Entity") ? tableName.Substring(0, tableName.Length - 6) : tableName;
            });

            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AspNetUsers { get; set; }
    }
}
