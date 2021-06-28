using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class GetProductUseCase : BaseUseCase<int, GetProductsResponse>
    {
        protected IProductRepository _productRepository { get; set; }

        public GetProductUseCase(IProductRepository productRepository, IPresenter<GetProductsResponse> presenter)
            : base(presenter)
        {
            _productRepository = productRepository;
        }

        public override async Task<IPresenter<GetProductsResponse>> HandleAsync(int request)
        {
            var product = await _productRepository.GetByIdAsync(request);

            _presenter.PresenterSuccess(new GetProductsResponse(product.Id, product.Title, product.ProductCategory.Title,
                product.Likes, product.IsTopRate, product.IsApproved, product.CreateDate.ToString("yyyy-MM-dd hh:mm:dd"),
                product.Description, product.ProductCategoryId));

            return _presenter;
        }
    }
}
