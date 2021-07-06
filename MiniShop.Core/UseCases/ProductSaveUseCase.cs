using MiniShop.Core.Domain;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class ProductSaveUseCase : BaseUseCase<ProductSaveRequest, bool>
    {
        protected IProductRepository _productRepository { get; set; }

        public ProductSaveUseCase(IProductRepository productRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _productRepository = productRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(ProductSaveRequest request)
        {
            var product = new Product();

            if (request.Id == 0)
            {
                product = new Product()
                {
                    CreateDate = DateTime.Now,
                    Description = request.Description,
                    IsApproved = false,
                    IsTopRate = request.IsTopRate,
                    Likes = 0,
                    ProductCategoryId = request.CategoryId,
                    Title = request.Title
                };

                await _productRepository.CreateAsync(product);
            }
            else
            {
                product = await _productRepository.GetByIdAsync(request.Id);
                product.Description = request.Description;
                product.IsTopRate = request.IsTopRate;
                product.ProductCategoryId = request.CategoryId;
                product.Title = request.Title;
                product.IsApproved = false;

                await _productRepository.EditAsync(product);
            }

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
