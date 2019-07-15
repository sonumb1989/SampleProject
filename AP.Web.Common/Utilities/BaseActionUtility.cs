using AP.Web.Common.Messages;

namespace AP.Web.Common.Utilities
{
    public class BaseActionUtility
  {
    protected BaseRequest Request { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseActionUtility"/> class.
    /// This constructure will be used for DI initiation
    /// </summary>
    public BaseActionUtility() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseActionUtility"/> class.
    /// This is development constructor
    /// </summary>
    /// <param name="request">request object</param>
    public BaseActionUtility(BaseRequest request)
    {
      Request = request;
    }
  }
}
