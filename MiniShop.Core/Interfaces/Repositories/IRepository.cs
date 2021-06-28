using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiniShop.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(int page, int pageSize);
        Task<int> CreateAsync(T entity);
        Task EditAsync(T entity);
        Task RemoveAsync(int id);
    }
}
