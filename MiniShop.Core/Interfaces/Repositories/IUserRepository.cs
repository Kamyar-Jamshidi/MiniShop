using MiniShop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniShop.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<List<User>> GetAllAsync();
        Task<string> CreateAsync(User entity);
        Task<User> LoginUserAsync(string username, string password);
        Task<User> GetByUsernameAsync(string username);
        Task ApproveUserAsync(string id);
    }
}
