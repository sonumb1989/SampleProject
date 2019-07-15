using System;

namespace AP.Web.Common.Messages
{
  /// <summary>
  /// This class contain default properties of incomming requests
  /// </summary>
  public class BaseRequest
  {
    #region RequestId

    /// <summary>
    /// This property is used to identify a given request from client side
    /// </summary>
    public string CorrelationId { get; set; }

    /// <summary>
    /// This property is used to identify a given request validation Id
    /// </summary>
    public string ValidationId { get; set; }

    #endregion

  }
}
