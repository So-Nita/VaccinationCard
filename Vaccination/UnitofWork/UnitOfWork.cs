using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Linq;
using Vaccination.EnityConfiguration;
using Vaccination.Repository;

namespace Vaccination.UnitofWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly VaccinationContext _context;
        private IDbContextTransaction? _dbTran;
        private readonly Dictionary<string, object> _repository = new();
        private bool _disposed;
        public UnitOfWork(VaccinationContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _dbTran?.Commit();
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _dbTran = _context.Database.BeginTransaction();
        }
        public void Rollback()
        {
            _dbTran?.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IRepository<TE, TK> GetRepository<TE, TK>() where TE : class 
        {
            var entityType = typeof(TE).Name;
            if (!_repository.ContainsKey(entityType))
            {
                try
                {
                    var repositoryType = typeof(Repository<TE, TK>);
                    var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                    if (repositoryInstance == null) throw new Exception();
                    _repository.Add(entityType, repositoryInstance);
                }
                catch
                {
                    throw new Exception();
                }
            }
            return (IRepository<TE, TK>)_repository[entityType];
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
