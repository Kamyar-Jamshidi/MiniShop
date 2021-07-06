using Microsoft.EntityFrameworkCore;
using MiniShop.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDBSet = MiniShop.Persistence.Entities.ProductCategory;
using EntityDomain = MiniShop.Core.Domain.ProductCategory;

namespace MiniShop.Persistence.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private MiniShopDB _context { get; }

        public ProductCategoryRepository(MiniShopDB context)
        {
            _context = context;
        }

        #region Privates

        private EntityDomain MapDbSetToDomain(EntityDBSet entityDBSet)
        {
            return new EntityDomain()
            {
                CreateDate = entityDBSet.CreateDate,
                Id = entityDBSet.Id,
                Title = entityDBSet.Title,
            };
        }
        private EntityDBSet MapDomainToDbSet(EntityDomain entityDomain)
        {
            return new EntityDBSet()
            {
                CreateDate = entityDomain.CreateDate,
                Id = entityDomain.Id,
                Title = entityDomain.Title,
            };
        }
        private async Task<EntityDBSet> FindByIdAsync(int id)
        {
            var entityDbSet = await _context.ProductCategories.SingleOrDefaultAsync(x => x.Id == id);
            if (entityDbSet == null)
                throw new Exception("Not Found");

            return entityDbSet;
        }

        #endregion

        #region Publics

        public async Task<EntityDomain> GetByIdAsync(int id)
        {
            var entityDBSet = await _context.ProductCategories.FindAsync(id);

            return MapDbSetToDomain(entityDBSet);
        }

        public async Task<List<EntityDomain>> GetAllAsync()
        {
            var list = await _context.ProductCategories.ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<int> CreateAsync(EntityDomain entity)
        {
            var entityDBSet = MapDomainToDbSet(entity);
            _context.ProductCategories.Add(entityDBSet);
            await _context.SaveChangesAsync();
            return entityDBSet.Id;
        }

        public async Task EditAsync(EntityDomain entity)
        {
            var entityDbSet = await FindByIdAsync(entity.Id);

            entityDbSet.Title = entity.Title;

            _context.ProductCategories.Update(entityDbSet);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entityDbSet = await FindByIdAsync(id);

            _context.ProductCategories.Remove(entityDbSet);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
