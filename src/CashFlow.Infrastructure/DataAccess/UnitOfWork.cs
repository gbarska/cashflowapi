using CashFlow.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly CashFlowDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _dbContext.SaveChanges();
                _transaction.Commit();
                Dispose();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                Dispose();
            }
        }
    }

}
