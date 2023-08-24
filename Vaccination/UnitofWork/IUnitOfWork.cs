using Vaccination.Repository;

namespace Vaccination.UnitofWork
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Dispose();
        IRepository<TE, TK> GetRepository<TE, TK>() where TE : class;
        void Rollback();
        void SaveChanges();
    }
}