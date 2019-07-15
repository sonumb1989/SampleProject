using AP.Web.Common.Extensions;
using AP.Web.Common.Messages;
using AP.Web.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Net;

namespace AP.Web.Common.Exceptions
{
    /// <summary>
    /// Exception Handling class
    /// </summary>
    public static class ExceptionHandling
    {
        private const string NotImplementExceptionMsgId = "M023";

        private const string InternalServerErrorMsgId = "M019";

        private const string InternalServerErrorCode = "500";

        /// <summary>
        /// Get exception info
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Service error</returns>
        public static ServiceError GetBaseExceptionInfo(this BaseException ex)
        {
            return ex.GetExceptionInfo(ex.GetMessageId(), ex.StatusCodeNumber.ToString(), ex.Params, true);
        }

        /// <summary>
        /// Get exception info
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="msgId">Message id</param>
        /// <param name="code">Status code</param>
        /// <param name="param">Parameters</param>
        /// <param name="isInternal">Check if exception is internal</param>
        /// <returns>Service error</returns>
        public static ServiceError GetExceptionInfo(this Exception ex, string msgId = null, string code = null, IDictionary<string, string> param = null, bool isInternal = false)
        {
            var result = new ServiceError()
            {
                MessageId = msgId,
#if DEBUG
                Message = ExceptionUtilities.GetFullExceptionMessage(ex),
#else
                Message = ex.Message,
#endif
                Params = param
            };

            if (isInternal == true)
            {
                result.InternalCode = code;
            }
            else
            {
                result.ErrorCode = code;

                if (code.IsEquals(InternalServerErrorCode))
                {
                    result.InternalCode = InternalServerErrorCode;
                }
            }

            return result;
        }

        /// <summary>
        /// Get exception info
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Service error</returns>
        public static ServiceError GetExceptionInfo(this Exception ex)
        {
            Type type = ex.GetType();
            ServiceError error = null;

            if (type.BaseType.IsEquals(typeof(BaseException)))
            {
                error = (ex as BaseException).GetBaseExceptionInfo();
            }
            else if (type.IsEquals(typeof(NotImplementedException)))
            {
                error = ex.GetExceptionInfo(NotImplementExceptionMsgId, ((int)HttpStatusCode.NotImplemented).ToString(), null, true);
            }
            else if (type.IsEquals(typeof(UnauthorizedAccessException)))
            {
                error = ex.GetExceptionInfo(null, ((int)HttpStatusCode.Forbidden).ToString());
            }
            else
            {
                error = ex.GetExceptionInfo(InternalServerErrorMsgId, ((int)HttpStatusCode.InternalServerError).ToString());
            }

            return error;
        }
    }
}
