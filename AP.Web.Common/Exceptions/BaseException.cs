using AP.Web.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Net;

namespace AP.Web.Common.Exceptions
{
    /// <summary>
    /// Base exception class
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Gets message id
        /// </summary>
        public virtual string MessageId
        {
            get
            {
                return (string)Data["MessageId"];
            }
        }

        /// <summary>
        /// Gets message id
        /// </summary>
        public abstract HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets status code number
        /// </summary>
        public int StatusCodeNumber
        {
            get
            {
                return int.Parse(StatusCode.ToString());
            }
        }

        /// <summary>
        /// Gets parameters
        /// </summary>
        public IDictionary<string, string> Params
        {
            get
            {
                return (IDictionary<string, string>)Data["Params"];
            }
        }

        /// <summary>
        /// Gets Message
        /// </summary>
        public override string Message
        {
            get
            {
                return ExceptionUtilities.GetExceptionResource((string)Data["MessageId"], (IDictionary<string, string>)Data["Params"]);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        public BaseException()
        : base()
        {
            Data.AddExceptionData(MessageId, StatusCode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="messageId">Message id</param>
        /// <param name="parameters">Parameters</param>
        public BaseException(string messageId, IDictionary<string, string> parameters = null)
        : base()
        {
            Data.AddExceptionData(messageId ?? MessageId, StatusCode, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="messageId">Message id</param>
        /// <param name="innerException">Inner exception</param>
        /// <param name="parameters">Parameters</param>
        public BaseException(string messageId, Exception innerException, IDictionary<string, string> parameters = null)
        : base(null, innerException)
        {
            Data.AddExceptionData(messageId ?? MessageId, StatusCode, parameters);
        }

        /// <summary>
        /// Gets message id
        /// </summary>
        /// <returns>Message id</returns>
        public string GetMessageId()
        {
            return (string)Data["MessageId"];
        }
    }
}
