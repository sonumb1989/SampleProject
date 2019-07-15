using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AP.Web.Persistence.Repository
{
  public interface IBaseRepository<TEntity>
  {
    #region [Custom repository using transaction scope]

    void Insert(TEntity entity);

    void Insert(params TEntity[] entities);

    void Insert(IEnumerable<TEntity> entities);

    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

    Task InsertAsync(params TEntity[] entities);

    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

    void Update(TEntity entity);

    void UpdateAsync(TEntity entity);

    void Update(params TEntity[] entities);

    void Update(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void Delete(object id);

    void Delete(params TEntity[] entities);

    void Delete(IEnumerable<TEntity> entities);

    void Complete();

    #endregion [Custom repository using transaction scope]

    #region [Repository Origin]

    void ChangeTable(string table);

    IQueryable<TEntity> GetAll();

    IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                      Func<IQueryable<TEntity>,
                                                      IIncludableQueryable<TEntity, object>> include = null,
                                                      int pageIndex = 0,
                                                      int pageSize = 20,
                                                      bool disableTracking = true);

    Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                int pageIndex = 0,
                                                                int pageSize = 20,
                                                                bool disableTracking = true,
                                                                CancellationToken cancellationToken = default(CancellationToken));

    IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                      Expression<Func<TEntity, bool>> predicate = null,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                      int pageIndex = 0,
                                                      int pageSize = 20,
                                                      bool disableTracking = true) where TResult : class;

    /// <summary>
    /// Gets the <see cref="IPagedList{TEntity}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="pageIndex">The index of page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                Expression<Func<TEntity, bool>> predicate = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                int pageIndex = 0,
                                                                int pageSize = 20,
                                                                bool disableTracking = true,
                                                                CancellationToken cancellationToken = default(CancellationToken)) where TResult : class;

    TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true);

    TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                              Expression<Func<TEntity, bool>> predicate = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool disableTracking = true);

    Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                        Expression<Func<TEntity, bool>> predicate = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                        bool disableTracking = true);

    Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                          bool disableTracking = true);

    IQueryable<TEntity> FromSql(string sql, params object[] parameters);

    TEntity Find(params object[] keyValues);

    Task<TEntity> FindAsync(params object[] keyValues);

    Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

    int Count(Expression<Func<TEntity, bool>> predicate = null);

    #endregion [Repository Origin]
  }
}
