using Microsoft.EntityFrameworkCore;
using MiniShop.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDBSet = MiniShop.Persistence.Entities.Admin;
using EntityDomain = MiniShop.Core.Domain.Admin;

namespace MiniShop.Persistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private MiniShopDB _context { get; }

        public AdminRepository(MiniShopDB context)
        {
            _context = context;
        }

        #region Privates

        private EntityDomain MapDbSetToDomain(EntityDBSet entityDBSet)
        {
            return new EntityDomain()
            {
                CreateDate = entityDBSet.CreateDate,
                FirstName = entityDBSet.FirstName,
                LastName = entityDBSet.LastName,
                Id = entityDBSet.Id,
                IsApproved = entityDBSet.IsApproved,
                IsSuperAdmin = entityDBSet.IsSuperAdmin,
                Password = entityDBSet.Password,
                Username = entityDBSet.Username,
                Token = entityDBSet.Token
            };
        }
        private EntityDBSet MapDomainToDbSet(EntityDomain entityDomain)
        {
            return new EntityDBSet()
            {
                CreateDate = entityDomain.CreateDate,
                FirstName = entityDomain.FirstName,
                LastName = entityDomain.LastName,
                Id = entityDomain.Id,
                IsApproved = entityDomain.IsApproved,
                IsSuperAdmin = entityDomain.IsSuperAdmin,
                Password = entityDomain.Password,
                Username = entityDomain.Username,
                Token = entityDomain.Token
            };
        }
        private async Task<EntityDBSet> FindByIdAsync(int id)
        {
            var entityDbSet = await _context.Admins.SingleOrDefaultAsync(x => x.Id == id);
            if (entityDbSet == null)
                throw new Exception("Not Found");

            return entityDbSet;
        }


        #endregion

        #region Publics

        public async Task<EntityDomain> GetByIdAsync(int id)
        {
            var entityDBSet = await _context.Admins.FindAsync(id);

            return MapDbSetToDomain(entityDBSet);
        }

        public async Task<List<EntityDomain>> GetAllAsync()
        {
            var list = await _context.Admins.Where(x => !x.IsSuperAdmin).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<List<EntityDomain>> GetAllAsync(int page, int pageSize)
        {
            var list = await _context.Admins.Where(x => !x.IsSuperAdmin).OrderByDescending(x => x.CreateDate)
                .Skip(page * pageSize).Take(pageSize).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<int> CreateAsync(EntityDomain entity)
        {
            var entityDBSet = MapDomainToDbSet(entity);
            _context.Admins.Add(entityDBSet);
            await _context.SaveChangesAsync();
            return entityDBSet.Id;
        }

        public async Task EditAsync(EntityDomain entity)
        {
            var entityDbSet = await FindByIdAsync(entity.Id);

            entityDbSet.FirstName = entity.FirstName;
            entityDbSet.LastName = entity.LastName;
            entityDbSet.IsApproved = entity.IsApproved;
            entityDbSet.Password = entity.Password;
            entityDbSet.Username = entity.Username;
            entityDbSet.Token = entity.Token;

            _context.Admins.Update(entityDbSet);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entityDbSet = await FindByIdAsync(id);

            _context.Admins.Remove(entityDbSet);
            await _context.SaveChangesAsync();
        }

        public async Task<EntityDomain> GetByCredentialInfoAsync(string username, string password)
        {
            var entityDbSet = await _context.Admins.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

            if (entityDbSet != null)
                return MapDbSetToDomain(entityDbSet);

            return null;
        }

        public async Task<EntityDomain> GetByTokenAsync(string token)
        {
            var entityDBSet = await _context.Admins.SingleOrDefaultAsync(x => x.Token == token);

            if (entityDBSet != null)
                return MapDbSetToDomain(entityDBSet);

            return null;
        }

        public async Task<EntityDomain> GetByUsernameAsync(string username)
        {
            var entityDBSet = await _context.Admins.SingleOrDefaultAsync(x => x.Username == username);

            if (entityDBSet != null)
                return MapDbSetToDomain(entityDBSet);

            return null;
        }

        #endregion
    }
}
