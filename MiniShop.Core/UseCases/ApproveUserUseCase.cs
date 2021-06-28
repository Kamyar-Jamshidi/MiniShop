using MiniShop.Core.DTO.Request;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class ApproveUserUseCase : BaseUseCase<ApproveUserRequest, bool>
    {
        protected IAdminRepository _adminRepository { get; set; }

        public ApproveUserUseCase(IAdminRepository adminRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(ApproveUserRequest request)
        {
            var admin = await _adminRepository.GetByTokenAsync(request.Token);

            if (admin == null || !admin.IsSuperAdmin)
            {
                _presenter.PresenterFail("You cant access this section!");
                return _presenter;
            }

            var selectedAdmin = await _adminRepository.GetByTokenAsync(request.UserToken);
            selectedAdmin.IsApproved = true;
            await _adminRepository.EditAsync(selectedAdmin);

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
