using AP.Web.Common.Messages;
using AP.Web.Persistence.Data;
using System.Threading.Tasks;

namespace AP.Web.API.Common
{
  public interface ICommandAction
  {
    Task<BaseServicesResult> ExecuteAction(IDataContext context);
  }
}
