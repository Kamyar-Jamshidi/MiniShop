using MiniShop.Core.Domain;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Core.Library;
using System;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class ProductSaveUseCase : BaseUseCase<ProductSaveRequest, bool>
    {
        protected IAdminRepository _adminRepository { get; set; }
        protected IProductRepository _productRepository { get; set; }

        public ProductSaveUseCase(IAdminRepository adminRepository, IProductRepository productRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
            _productRepository = productRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(ProductSaveRequest request)
        {
            var admin = await _adminRepository.GetByTokenAsync(request.Token);

            if (admin == null)
            {
                _presenter.PresenterFail("You cant access this section!");
                return _presenter;
            }

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
