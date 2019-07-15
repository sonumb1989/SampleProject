namespace AP.Web.Persistence.UnitOfWork
{
  public class UnitOfWorkDbOption : IUnitOfWorkDbOption
  {
    internal static int CommandTimeoutInSecond = 10;
    internal static readonly bool IsSqlServer = true;
    internal static readonly bool IsSqlAnyWhere = false;
  }
}
