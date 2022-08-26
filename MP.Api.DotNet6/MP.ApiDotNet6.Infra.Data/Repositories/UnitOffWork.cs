using Microsoft.EntityFrameworkCore.Storage;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;

namespace MP.ApiDotNet6.Infra.Data.Repositories
{
    public class UnitOffWork : IUnitOffWork
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction _dbContextTransaction;
        public UnitOffWork(ApplicationDbContext db)
        {
            _db = db; 
        }

        public async Task BeginTransaction()
        {
            _dbContextTransaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
           await _dbContextTransaction.CommitAsync();
        }
        public async Task RollBack()
        {
            await _dbContextTransaction.RollbackAsync();
        }

        public void Dispose()
        {
            _dbContextTransaction?.Dispose();
        }

    }
}
