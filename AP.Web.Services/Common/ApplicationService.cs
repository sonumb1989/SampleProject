using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using AP.Web.Persistence.UnitOfWork;

namespace AP.Web.Services.Common
{
    public abstract class ApplicationService : IApplicationService
    {
        /// <summary>
        /// Usually we need to use constructor injection, but for base classes, we decided to use property injection.
        /// The benefit of this usage is avoiding bloating constructor injections.
        /// </summary>
        [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty", Justification = "Only framework team can decide to use property injection.")]
        //[Experimental("These flows will be provided by property injection.")]
        public IUnitOfWorkProvider Provider { get; set; }

        /// <summary>
        /// Provide
        /// </summary>
        /// <param name="dbOption">dbOption</param>
        /// <param name="scopeOption">scopeOption</param>
        /// <param name="isolationLevel">isolationLevel</param>
        /// <returns></returns>
        protected IUnitOfWork Provide(IUnitOfWorkDbOption dbOption, UnitOfWorkScopeOption scopeOption = UnitOfWorkScopeOption.Required,
          IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Provider.Provide(dbOption, scopeOption, isolationLevel);
        }

        /// <summary>
        /// ProvideForCompany
        /// </summary>
        /// <param name="scopeOption">scopeOption</param>
        /// <param name="isolationLevel">isolationLevel</param>
        /// <returns></returns>
        protected IUnitOfWork Provide(UnitOfWorkScopeOption scopeOption = UnitOfWorkScopeOption.Required,
          IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return Provider.Provide(scopeOption, isolationLevel);
        }

        /// <summary>
        /// PerformQueries
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="func">function callback</param>
        /// <param name="unitOfWorkDbOption">unitOfWorkDbOption</param>
        /// <returns></returns>
        protected T PerformQueries<T>(Func<T> func, IUnitOfWorkDbOption unitOfWorkDbOption)
        {
            using (var uow = Provide(unitOfWorkDbOption))
            {
                T result = func();
                uow.Complete();
                return result;
            }
        }

        /// <summary>
        /// PerformActions
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="unitOfWorkDbOption">unitOfWorkDbOption</param>
        protected void PerformActions(Action action, IUnitOfWorkDbOption unitOfWorkDbOption)
        {
            using (var uow = Provide(unitOfWorkDbOption))
            {
                action();
                uow.Complete();
            }
        }

        /// <summary>
        /// PerformActionsForCompany
        /// </summary>
        /// <param name="action">action</param>
        protected void PerformActionsForCompany(Action action)
        {
            using (var uow = Provide())
            {
                action();
                uow.Complete();
            }
        }
    }
}