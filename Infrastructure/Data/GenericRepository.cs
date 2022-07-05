using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;

        }

        public async Task<T> GetProductByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(p=> p.Id ==id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            var l = await _context.Set<T>().ToListAsync();
            return l;
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
         
        private IQueryable<T> ApplySpecification(ISpecification<T> spec){
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}