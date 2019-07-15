using System;
using System.Threading;

namespace AP.Web.Common.Base
{
    public class Disposable : IDisposable
    {
        #region IDisposable Support

        /// <summary>
        /// isDisposed variable
        /// </summary>
        protected int isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Disposable"/> class.
        /// </summary>
        protected Disposable()
        {
            isDisposed = 0;
        }

        /// <summary>
        /// Dispose action
        /// </summary>
        public virtual void Dispose()
        {
            // 1 indicates that the current instance is already disposed.
            if (Interlocked.Exchange(ref isDisposed, 1) != 1)
            {
                try
                {
                    Dispose(true);
                }
                catch
                {
                }

                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// This code added to correctly implement the disposable pattern.
        /// </summary>
        /// <param name="bManaged">bool</param>
        protected virtual void Dispose(bool bManaged)
        {
        }

        #endregion IDisposable Support
    }
}
