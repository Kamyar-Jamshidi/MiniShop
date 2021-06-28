using MiniShop.Core.DTO.Request;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class ChangeProductStatusUseCase : BaseUseCase<ChangeProductStatusRequest, bool>
    {
        protected IAdminRepository _adminRepository { get; set; }
        protected IProductRepository _productRepository { get; set; }

        public ChangeProductStatusUseCase(IAdminRepository adminRepository, IProductRepository productRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
            _productRepository = productRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(ChangeProductStatusRequest request)
        {
            var admin = await _adminRepository.GetByTokenAsync(request.Token);

            if (admin == null || !admin.IsSuperAdmin)
            {
                _presenter.PresenterFail("You cant access this section!");
                return _presenter;
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            product.IsApproved = !product.IsApproved;
            await _productRepository.EditAsync(product);

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
