using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetProductByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAsync();
    }
}