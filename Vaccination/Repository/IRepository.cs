using Vaccination.UnitofWork;

namespace Vaccination.Repository
{
    public interface IRepository<TE, TK> where TE : class //, IKey<TK>
    {
        void Add(TE entity);
        Task AddAsync(TE entity);
        void AddRange(IEnumerable<TE> entities);
        Task AddRangeAsync(IEnumerable<TE> entities);
        void Delete(TE entity);
        Task DeleteAsync(TE entity);
        IQueryable<TE>? GetAll();
        Task<IQueryable<TE?>> GetAllAsync();
        TE? GetById(TK id);
        Task<TE> GetByIdAsync(TK id);
        void Update(TE entity);
        Task UpdateAsync(TE entity);
    }
}