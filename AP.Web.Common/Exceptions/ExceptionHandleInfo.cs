using System;

namespace AP.Web.Common.Exceptions
{
  public class ExceptionHandleInfo
  {
    /// <summary>
    /// Gets or sets the FuncCaller
    /// </summary>
    public string FuncCaller { get; set; }

    /// <summary>
    ///  Gets or sets the FileName
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets type Name
    /// </summary>
    public string TypeName { get; set; }

    /// <summary>
    /// Gets or sets the LineNumber
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// Gets or sets  Messages
    /// </summary>
    public string Messages { get; set; }

    /// <summary>
    /// Gets or sets MessageId
    /// </summary>
    public string MessageId { get; set; }

    /// <summary>
    /// Gets or sets FullMessage
    /// </summary>
    public string FullMessage { get; set; }

    /// <summary>
    /// Gets or sets httpStatusCode
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets FieldName
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets applicationId
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// Gets or sets Params
    /// </summary>
    public string Params { get; set; }
  }
}
