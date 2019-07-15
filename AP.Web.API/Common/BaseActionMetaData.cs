using System.Reflection;

namespace AP.Web.API.Common
{
  public class BaseActionMetaData<TBusiness>
    where TBusiness : class
  {
    public string ActionName { get; set; }
    public string MethodName { get; set; }
    public MethodBase Method { get; set; }
    public TBusiness BusinessLogic { get; set; }
    public BaseActionMetaData(BaseActionCommand<TBusiness> action)
    {
      ActionName = action.GetType().Name;
      MethodName = ActionName.Substring(0, ActionName.Length - 6);
      Method = typeof(TBusiness).GetMethod(MethodName);
      BusinessLogic = action.BusinessLogic;
    }
  }
}
