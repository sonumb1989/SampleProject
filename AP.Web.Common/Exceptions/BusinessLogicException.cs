using System;
using System.Collections.Generic;
using System.Net;

namespace AP.Web.Common.Exceptions
{
  /// <summary>
  /// Business Logic exception class
  /// </summary>
  public class BusinessLogicException : BaseException
  {
    /// <summary>
    /// Gets message id
    /// </summary>
    public override HttpStatusCode StatusCode
    {
      get
      {
        return HttpStatusCode.ExpectationFailed;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessLogicException"/> class.
    /// </summary>
    public BusinessLogicException()
    : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessLogicException"/> class.
    /// </summary>
    /// <param name="messageId">Message id</param>
    /// <param name="parameters">Parameters</param>
    public BusinessLogicException(string messageId, IDictionary<string, string> parameters = null)
    : base(messageId, parameters)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessLogicException"/> class.
    /// </summary>
    /// <param name="messageId">Message id</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="parameters">Parameters</param>
    public BusinessLogicException(string messageId, Exception innerException, IDictionary<string, string> parameters = null)
    : base(messageId, innerException, parameters)
    {
    }
  }
}
