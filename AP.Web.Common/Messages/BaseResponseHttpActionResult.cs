namespace AP.Web.Common.Messages
{
  /// <summary>
  /// class BaseResponseHttpActionResult
  /// </summary>
  /// <typeparam name="T">Class</typeparam>
  public class BaseResponseHttpActionResult<T>
  {
    /// <summary>
    /// Gets or sets result
    /// </summary>
    public T Result { get; set; }

    /// <summary>
    /// Gets or sets internal code
    /// </summary>
    public int? InternalCode { get; set; }
  }
}
