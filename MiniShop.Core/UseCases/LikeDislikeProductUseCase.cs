using MiniShop.Core.DTO.Request;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class LikeDislikeProductUseCase : BaseUseCase<LikeDislikeProductRequest, bool>
    {
        protected IProductRepository _productRepository { get; set; }

        public LikeDislikeProductUseCase(IProductRepository productRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _productRepository = productRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(LikeDislikeProductRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            product.Likes = request.Like ? product.Likes + 1 : product.Likes - 1;
            await _productRepository.EditAsync(product);

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
