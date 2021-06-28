using Microsoft.EntityFrameworkCore;
using MiniShop.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDBSet = MiniShop.Persistence.Entities.Product;
using EntityDomain = MiniShop.Core.Domain.Product;

namespace MiniShop.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private MiniShopDB _context { get; }

        public ProductRepository(MiniShopDB context)
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
                Description = entityDBSet.Description,
                IsTopRate = entityDBSet.IsTopRate,
                IsApproved = entityDBSet.IsApproved,
                Likes = entityDBSet.Likes,
                ProductCategoryId = entityDBSet.ProductCategoryId,
                Title = entityDBSet.Title,
                ProductCategory = new Core.Domain.ProductCategory() {
                    Title = entityDBSet.ProductCategory.Title,
                    Id = entityDBSet.ProductCategory.Id
                }
            };
        }
        private EntityDBSet MapDomainToDbSet(EntityDomain entityDomain)
        {
            return new EntityDBSet()
            {
                CreateDate = entityDomain.CreateDate,
                Id = entityDomain.Id,
                Description = entityDomain.Description,
                IsTopRate = entityDomain.IsTopRate,
                IsApproved = entityDomain.IsApproved,
                Likes = entityDomain.Likes,
                ProductCategoryId = entityDomain.ProductCategoryId,
                Title = entityDomain.Title
            };
        }
        private async Task<EntityDBSet> FindByIdAsync(int id)
        {
            var entityDbSet = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (entityDbSet == null)
                throw new Exception("Not Found");

            return entityDbSet;
        }


        #endregion

        #region Publics

        public async Task<EntityDomain> GetByIdAsync(int id)
        {
            var entityDBSet = await _context.Products.Include(x => x.ProductCategory).SingleOrDefaultAsync(z => z.Id == id);

            return MapDbSetToDomain(entityDBSet);
        }

        public async Task<List<EntityDomain>> GetAllAsync()
        {
            var list = await _context.Products.Include(x => x.ProductCategory).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<List<EntityDomain>> GetAllAsync(int page, int pageSize)
        {
            var list = await _context.Products.OrderByDescending(x => x.CreateDate)
                .Skip(page * pageSize).Take(pageSize).Include(x => x.ProductCategory).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<int> CreateAsync(EntityDomain entity)
        {
            var entityDBSet = MapDomainToDbSet(entity);
            _context.Products.Add(entityDBSet);
            await _context.SaveChangesAsync();
            return entityDBSet.Id;
        }

        public async Task EditAsync(EntityDomain entity)
        {
            var entityDbSet = await FindByIdAsync(entity.Id);

            entityDbSet.Description = entity.Description;
            entityDbSet.IsTopRate = entity.IsTopRate;
            entityDbSet.Likes = entity.Likes;
            entityDbSet.ProductCategoryId = entity.ProductCategoryId;
            entityDbSet.Title = entity.Title;
            entityDbSet.IsApproved = entity.IsApproved;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entityDbSet = await FindByIdAsync(id);

            _context.Products.Remove(entityDbSet);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EntityDomain>> GetAllNotApprovedAsync()
        {
            var list = await _context.Products.Where(x => !x.IsApproved).Include(x => x.ProductCategory).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<List<EntityDomain>> GetAllTopRateAsync()
        {
            var list = await _context.Products.Where(x => x.IsApproved && x.IsTopRate).Include(x => x.ProductCategory).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        public async Task<List<EntityDomain>> GetAllNewAsync(int days)
        {
            var list = await _context.Products.Where(x => x.IsApproved && EF.Functions.DateDiffDay(x.CreateDate.Date, DateTime.Now) <= days)
                .Include(x => x.ProductCategory).ToListAsync();

            return list.Select(x => MapDbSetToDomain(x)).ToList();
        }

        #endregion
    }
}
