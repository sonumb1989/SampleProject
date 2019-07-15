using AP.Web.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace AP.Web.Common.Reflection
{
    public static class BaseDbSetContext
  {
    /// <summary>
    /// Set
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="T">Type</param>
    /// <returns></returns>
    public static IQueryable Set(this DbContext context, Type T)
    {
      // Get the generic type definition
      MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

      // Build a method with the specific type argument you're interested in
      method = method.MakeGenericMethod(T);

      return method.Invoke(context, null) as IQueryable;
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    /// <param name="context">context</param>
    /// <returns></returns>
    public static IQueryable<TEntity> Set<TEntity>(this DbContext context)
    {
      // Get the generic type definition 
      MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

      // Build a method with the specific type argument you're interested in 
      method = method.MakeGenericMethod(typeof(TEntity));

      return method.Invoke(context, null) as IQueryable<TEntity>;
    }

    /// <summary>
    /// CreateDbSet
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    /// <param name="db">db</param>
    /// <param name="entityToCreate">entityToCreate</param>
    public static void CreateDbSet<TEntity>(this DbContext db, TEntity entityToCreate)
    where TEntity : class
    {
      if (!CheckEntityExisting(db, entityToCreate))
      {
        db.Set<TEntity>().Add(entityToCreate);
        db.SaveChanges();
      }
    }

    private static bool CheckEntityExisting<TEntity>(this DbContext db, TEntity entityToCreate)
      where TEntity : class
    {
      return db.Set<TEntity>().Local.Any(e => e.IsEquals(entityToCreate));
    }
  }
}
