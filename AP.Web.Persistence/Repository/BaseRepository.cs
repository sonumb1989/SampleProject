using AP.Web.Common.Extensions;
using AP.Web.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AP.Web.Persistence.Repository
{
    public class BaseRepository<TEntity> : Repository<TEntity>, IBaseRepository<TEntity>
    where TEntity : class
    {
        public ITransactionScope _transaction;
        public BaseRepository(DbContext context)
          : base(context)
        {
            _transaction = StartupExtensions.Resolve<ITransactionScope>();
        }

        #region [Custom Repository using transaction scope]

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public override void Insert(TEntity entity)
        {
            base.Insert(entity);
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public override void Insert(params TEntity[] entities)
        {
            //_transaction.BeginTransaction();
            base.Insert(entities);
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public override void Insert(IEnumerable<TEntity> entities)
        {
            //_transaction.BeginTransaction();
            base.Insert(entities);
        }

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        public override Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            //_transaction.BeginTransaction();
            return base.InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="Task" /> that represents the asynchronous insert operation.</returns>
        public override Task InsertAsync(params TEntity[] entities)
        {
            //_transaction.BeginTransaction();
            return base.InsertAsync(entities);
        }

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        public override Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            //_transaction.BeginTransaction();
            return base.InsertAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Update(TEntity entity)
        {
            //_transaction.BeginTransaction();
            base.Update(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void UpdateAsync(TEntity entity)
        {
            //_transaction.BeginTransaction();
            base.UpdateAsync(entity);
        }

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public override void Update(params TEntity[] entities)
        {
            //_transaction.BeginTransaction();
            base.Update(entities);
        }

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public override void Update(IEnumerable<TEntity> entities)
        {
            //_transaction.BeginTransaction();
            base.Update(entities);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public override void Delete(TEntity entity)
        {
            //_transaction.BeginTransaction();
            base.Delete(entity);
        }

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        public override void Delete(object id)
        {
            _transaction.BeginTransaction();
            base.Delete(id);
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public override void Delete(params TEntity[] entities)
        {
            _transaction.BeginTransaction();
            base.Delete(entities);
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public override void Delete(IEnumerable<TEntity> entities)
        {
            _transaction.BeginTransaction();
            base.Delete(entities);
        }

        public void Complete()
        {
            SaveChanges();
            //_transaction.Complete();
        }

        #endregion [Custom Repository using transaction scope]

        #region [Repository Origin]

        public override void ChangeTable(string table)
        {
            base.ChangeTable(table);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return base.GetAll();
        }

        public override IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>,
                                                          IIncludableQueryable<TEntity, object>> include = null,
                                                          int pageIndex = 0,
                                                          int pageSize = 20,
                                                          bool disableTracking = true)
        {
            return base.GetPagedList(predicate, orderBy, include, pageIndex, pageSize, disableTracking);
        }

        public override Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                    int pageIndex = 0,
                                                                    int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetPagedListAsync(predicate, orderBy, include, pageIndex, pageSize, disableTracking, cancellationToken);
        }

        public override IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                          Expression<Func<TEntity, bool>> predicate = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                          int pageIndex = 0,
                                                          int pageSize = 20,
                                                          bool disableTracking = true)
        {
            return base.GetPagedList(selector, predicate, orderBy, include, pageIndex, pageSize, disableTracking);
        }

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
        public override Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                    Expression<Func<TEntity, bool>> predicate = null,
                                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                    int pageIndex = 0,
                                                                    int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetPagedListAsync(selector, predicate, orderBy, include, pageIndex, pageSize, disableTracking, cancellationToken);
        }

        public override TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true)
        {
            return base.GetFirstOrDefault(predicate, orderBy, include, disableTracking);
        }

        public override TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true)
        {
            return base.GetFirstOrDefault(selector, predicate, orderBy, include, disableTracking);
        }

        public override Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                            Expression<Func<TEntity, bool>> predicate = null,
                                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                            bool disableTracking = true)
        {
            return base.GetFirstOrDefaultAsync(selector, predicate, orderBy, include, disableTracking);
        }

        public override Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                              bool disableTracking = true)
        {
            return base.GetFirstOrDefaultAsync(predicate, orderBy, include, disableTracking);
        }

        public override IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return base.FromSql(sql, parameters);
        }

        public override TEntity Find(params object[] keyValues)
        {
            return base.Find(keyValues);
        }

        public override Task<TEntity> FindAsync(params object[] keyValues)
        {
            return base.FindAsync(keyValues);
        }

        public override Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return base.FindAsync(keyValues, cancellationToken);
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return base.Count(predicate);
        }

        #endregion [Repository Origin]

        private int SaveChanges()
        {
            // call base context saving operation to insert all Transactions
            return _dbContext.SaveChanges();
        }

        private Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // call base context saving operation to insert all Transactions
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
