using AP.Web.Common.Extensions;
using AP.Web.Persistence.Data;
using AP.Web.Persistence.UnitOfWork;
using AutoMapper;
using System;

namespace AP.Web.Services.Common
{
    /// <summary>
    ///     BaseBusinessLogic Class
    /// </summary>
    public abstract class BaseBusinessLogic : /*ApplicationService,*/ IBaseBusinessLogic, IDisposable
    {
        #region Public Properties

        /// <summary>
        /// Gets data context
        /// </summary>
        public IDataContext DataContext { get; }

        /// <summary>
        /// UnitOfWork
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Create instance for auto mapper
        /// </summary>
        public IMapper AutoMapper => Mapper.Instance;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseBusinessLogic" /> class.
        ///     BaseBusinessLogic
        /// </summary>
        /// <param name="dataContext">IDataContext</param>
        protected BaseBusinessLogic(IDataContext dataContext)
        {
            UnitOfWork = StartupExtensions.Resolve<IUnitOfWork>();
            //Provider = StartupExtensions.Resolve<IUnitOfWorkProvider>();
            DataContext = dataContext;
            ConfigAutoMapper();
        }

        public T BizManager<T>()
        {
            return StartupExtensions.Resolve<T>();
        }

        #endregion Constructors

        #region Abstract Method

        protected abstract void ConfigAutoMapper();

        #endregion Abstract Method

        public bool HealthCheck()
        {
            return true;
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
