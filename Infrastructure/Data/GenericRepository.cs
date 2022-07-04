using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
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

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            var l = await _context.Set<T>().ToListAsync();
            return l;
        }

        public async Task<T> GetProductByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(p=> p.Id ==id);
        }
    }
}