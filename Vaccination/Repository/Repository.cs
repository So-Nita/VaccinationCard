using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Drawing.Printing;
using Vaccination.EnityConfiguration;
using Vaccination.UnitofWork;

namespace Vaccination.Repository
{
    public class Repository<TE, TK> : IRepository<TE, TK> where TE : class //, IKey<TK>
    {
        private readonly VaccinationContext _context;
        public Repository(VaccinationContext context)
        {
            _context = context;
        }

        public IQueryable<TE>? GetAll()
        {
            return _context.Set<TE>().AsQueryable();
        }
        public TE? GetById(TK id)
        {
            return _context.Set<TE>().Find(id);
        }

        public void Add(TE entity)
        {
            _context.Set<TE>().Add(entity);
        }
        public void AddRange(IEnumerable<TE> entities)
        {
            _context.Set<TE>().AddRange(entities);
        }
        public void Update(TE entity)
        {
            _context.Set<TE>().Update(entity);
        }
        public void Delete(TE entity)
        {
            _context.Set<TE>().Remove(entity);
        }

        public async Task<IQueryable<TE?>> GetAllAsync()
        {
            return await Task.Run(() => GetAll()!);
        }
        public async Task<TE> GetByIdAsync(TK id)
        {
            return await Task.Run(() => GetById(id)!);
        }
        public async Task AddAsync(TE entity)
        {
            await Task.Run(() => Add(entity));
        }
        public async Task AddRangeAsync(IEnumerable<TE> entities)
        {
            await Task.Run(() => AddRange(entities));
        }
        public async Task UpdateAsync(TE entity)
        {
            await Task.Run(() => Update(entity));
        }
        public async Task DeleteAsync(TE entity)
        {
            await Task.Run(() => Delete(entity));
        }
    }
}
