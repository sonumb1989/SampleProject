using Newtonsoft.Json;
using System.Collections.Generic;

namespace AP.Web.Common.Messages
{
  public class ServiceErrors: List<ServiceError>
  {
  }

  /// <summary>
  /// An error response intended for validation errors etc.
  /// </summary>
  public class ServiceError
  {
    /// <summary>
    /// Gets or sets error code
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets knows internal code
    /// </summary>
    [JsonIgnore]
    public string InternalCode { get; set; }

    /// <summary>
    /// Gets or sets description
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets message id
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    /// Gets or sets field name
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets parameters
    /// </summary>
    public IDictionary<string, string> Params { get; set; }
  }
}
