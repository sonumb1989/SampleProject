using System;
using System.Net;
namespace AP.Web.Common.Messages
{
  /// <summary>
  /// This class contain default properties per action
  /// </summary>
  public class BaseServicesResult
  {
    /// <summary>
    /// Gets or sets ReturnCode
    /// </summary>
    public HttpStatusCode ReturnCode { get; set; }

    /// <summary>
    /// Gets or sets ReturnData
    /// </summary>
    public object ReturnData { get; set; }

    private DateTime _timestamp;

    /// <summary>
    /// Gets or sets timeStamp
    /// </summary>
    [Obsolete]
    public DateTime TimeStamp
    {
      get
      {
        _timestamp = DateTime.UtcNow;
        return _timestamp;
      }
      set => _timestamp = value;
    }
  }
}
