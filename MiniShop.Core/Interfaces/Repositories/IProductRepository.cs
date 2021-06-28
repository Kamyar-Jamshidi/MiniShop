using MiniShop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniShop.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllNotApprovedAsync();
        Task<List<Product>> GetAllTopRateAsync();
        Task<List<Product>> GetAllNewAsync(int days);
    }
}
