using AP.Web.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace AP.Web.Persistence.Data
{
    public class TransactionScope : ITransactionScope, IDisposable
  {
    protected bool IsCompleted { get; private set; }
    private readonly DbContext _context;
    private readonly UnitOfWorkScopeOption _scopeOption;
    private readonly IsolationLevel _isolationLevel;
    private IDbTransaction _transaction;
    private IDbConnection _connection;
    public IDbTransaction Transaction => _transaction;

    public TransactionScope(DbContext context, UnitOfWorkScopeOption scopeOption = UnitOfWorkScopeOption.Required,
      IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _scopeOption = scopeOption;
      _isolationLevel = isolationLevel;
      _connection = _context.Database.GetDbConnection();
    }

    /// <summary>
    /// BeginTransaction
    /// </summary>
    public void BeginTransaction()
    {
      if (!_connection.State.HasFlag(ConnectionState.Open))
      {
        _connection.Open();
      }

            if (UnitOfWorkDbOption.IsSqlAnyWhere || UnitOfWorkDbOption.IsSqlServer)
            {
                IDbCommand setTimeoutCmd = _connection.CreateCommand();
                setTimeoutCmd.CommandText = UnitOfWorkDbOption.IsSqlAnyWhere
                    ? $"SET TEMPORARY OPTION request_timeout={UnitOfWorkDbOption.CommandTimeoutInSecond};"
                    : $"EXEC sp_configure 'remote query timeout', {UnitOfWorkDbOption.CommandTimeoutInSecond};";

                setTimeoutCmd.ExecuteNonQuery();
            }

            // No ambient transaction
            if (System.Transactions.Transaction.Current == null && Transaction == null)
      {
        _transaction = _connection.BeginTransaction(_isolationLevel);
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected void CommitTransaction()
    {
      if (_transaction == null)
      {
        return;
      }

      try
      {
        _transaction.Commit();
      }
      finally
      {
        _transaction.Dispose();
      }
    }

    protected void RollbackTransaction()
    {
      try
      {
        _transaction?.Rollback();
      }
      finally
      {
        _transaction?.Dispose();
        _connection.Close();
      }
    }
    protected virtual void Dispose(bool disposing)
    {
      try
      {
        if (!IsCompleted)
        {
          RollbackTransaction();
        }
        else
        {
          try
          {
            CommitTransaction();

            // dispose the db context.
            _context.Dispose();
          }
          catch
          {
            RollbackTransaction();
            throw;
          }
        }
      }
      finally
      {
        DisposeConnection();
      }
    }

    public void Complete()
    {
      IsCompleted = true;
      Dispose();
    }

    private void DisposeConnection()
    {
      try
      {
        _connection.Close();
      }
      finally
      {
        _connection.Dispose();
      }
    }
  }
}
