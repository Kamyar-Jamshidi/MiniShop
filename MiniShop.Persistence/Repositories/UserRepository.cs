using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniShop.Core.DTO;
using MiniShop.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbSetUser = MiniShop.Persistence.Entities.User;
using DomainUser = MiniShop.Core.Domain.User;

namespace MiniShop.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MiniShopDB _context { get; }
        private UserManager<DbSetUser> _userManager { get; }
        private SignInManager<DbSetUser> _signInManager { get; }

        public UserRepository(MiniShopDB context,
            UserManager<DbSetUser> userManager,
            SignInManager<DbSetUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Privates

        private DomainUser MapDbSetToDomain(DbSetUser dbSetUser, bool isAdmin)
        {
            return new DomainUser()
            {
                CreateDate = dbSetUser.CreateDate,
                FirstName = dbSetUser.FirstName,
                LastName = dbSetUser.LastName,
                Id = dbSetUser.Id,
                IsApproved = dbSetUser.IsApproved,
                Username = dbSetUser.UserName,
                IsAdmin = isAdmin
            };
        }
        private DbSetUser MapDomainToDbSet(DomainUser domainUser)
        {
            return new DbSetUser()
            {
                CreateDate = domainUser.CreateDate,
                FirstName = domainUser.FirstName,
                LastName = domainUser.LastName,
                Id = domainUser.Id,
                IsApproved = domainUser.IsApproved,
                UserName = domainUser.Username
            };
        }

        #endregion

        #region Publics

        public async Task<DomainUser> GetByIdAsync(string id)
        {
            var dbSetUser = await _userManager.FindByIdAsync(id);
            var isAdmin = await _userManager.IsInRoleAsync(dbSetUser, Role.Admin);
            return MapDbSetToDomain(dbSetUser, isAdmin);
        }

        public async Task<List<DomainUser>> GetAllAsync()
        {
            var list = await _userManager.Users.ToListAsync();
            var result = new List<DomainUser>();

            foreach (var item in list)
            {
                var isAdmin = _userManager.IsInRoleAsync(item, Role.Admin).Result;
                if (!isAdmin)
                {
                    result.Add(MapDbSetToDomain(item, isAdmin));
                }
            }

            return result;
        }

        public async Task<string> CreateAsync(DomainUser entity)
        {
            var dbSetUser = MapDomainToDbSet(entity);
            dbSetUser.Id = Guid.NewGuid().ToString();
            await _userManager.CreateAsync(dbSetUser, entity.Password);
            await _userManager.AddToRoleAsync(dbSetUser, Role.Operator);
            return dbSetUser.Id;
        }

        public async Task<DomainUser> GetByUsernameAsync(string username)
        {
            var dbSetUser = await _userManager.FindByNameAsync(username);

            if (dbSetUser != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(dbSetUser, Role.Admin);
                return MapDbSetToDomain(dbSetUser, isAdmin);
            }

            return null;
        }

        public async Task<DomainUser> LoginUserAsync(string username, string password)
        {
            var dbSetUser = await _userManager.FindByNameAsync(username);

            if (dbSetUser == null)
                return null;

            var isValidate = await _signInManager.UserManager.CheckPasswordAsync(dbSetUser, password);
            if (!isValidate)
                return null;

            var roles = await _userManager.GetRolesAsync(dbSetUser);
            var domainUser = MapDbSetToDomain(dbSetUser, roles.Contains(Role.Admin));
            domainUser.Roles = roles;
            return domainUser;
        }

        public async Task ApproveUserAsync(string id)
        {
            var dbSetUser = await _userManager.FindByIdAsync(id);
            dbSetUser.IsApproved = true;
            await _userManager.UpdateAsync(dbSetUser);
        }

        #endregion
    }
}
