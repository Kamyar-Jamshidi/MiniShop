using MiniShop.Core.Domain;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MiniShop.Core.UseCases.GetAllProductsUseCase;

namespace MiniShop.Core.UseCases
{
    public class GetAllProductsUseCase : BaseUseCase<ProductTypes, List<GetProductsResponse>>
    {
        protected IProductRepository _productRepository { get; set; }

        public GetAllProductsUseCase(IProductRepository productRepository, IPresenter<List<GetProductsResponse>> presenter)
            : base(presenter)
        {
            _productRepository = productRepository;
        }
        
        public enum ProductTypes
        {
            All,
            Top,
            New
        }
        
        public override async Task<IPresenter<List<GetProductsResponse>>> HandleAsync(ProductTypes request)
        {
            var list = new List<Product>();

            switch (request)
            {
                case ProductTypes.All:
                    list  = await _productRepository.GetAllAsync();
                    break;
                case ProductTypes.Top:
                    list = await _productRepository.GetAllTopRateAsync();
                    break;
                case ProductTypes.New:
                    list = await _productRepository.GetAllNewAsync(14);
                    break;
            }

            _presenter.PresenterSuccess(list.Select(x => new GetProductsResponse(x.Id, x.Title,
                x.ProductCategory.Title, x.Likes, x.IsTopRate, x.IsApproved,
                x.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"), x.Description, x.ProductCategoryId)).ToList());

            return _presenter;
        }
    }
}
