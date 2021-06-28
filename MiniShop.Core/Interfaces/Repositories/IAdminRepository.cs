using MiniShop.Core.Domain;
using System.Threading.Tasks;

namespace MiniShop.Core.Interfaces.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> GetByTokenAsync(string token);
        Task<Admin> GetByCredentialInfoAsync(string username, string password);
        Task<Admin> GetByUsernameAsync(string username);
    }
}
