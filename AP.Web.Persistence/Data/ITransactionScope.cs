namespace AP.Web.Persistence.Data
{
  public interface ITransactionScope
  {
    void BeginTransaction();
    void Dispose();
    void Complete();
  }
}
