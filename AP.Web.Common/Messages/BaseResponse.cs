namespace AP.Web.Common.Messages
{
  public class BaseResponse
  {
    /// <summary>
    /// Gets or sets a value indicating whether
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets Errors
    /// </summary>
    public ServiceErrors Errors { get; set; }

    /// <summary>
    /// Gets or sets data
    /// </summary>
    public string RecordState { get; set; }
  }
}
