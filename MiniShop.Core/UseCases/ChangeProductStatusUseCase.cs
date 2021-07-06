using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class ChangeProductStatusUseCase : BaseUseCase<int, bool>
    {
        protected IProductRepository _productRepository { get; set; }

        public ChangeProductStatusUseCase(IProductRepository productRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _productRepository = productRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(int request)
        {
            var product = await _productRepository.GetByIdAsync(request);
            product.IsApproved = !product.IsApproved;
            await _productRepository.EditAsync(product);

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
