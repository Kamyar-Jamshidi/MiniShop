using MiniShop.Core.DTO.Request;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Core.Library;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class CheckAuthDataUseCase : BaseUseCase<LoginRequest, LoginResponse>
    {
        protected IAdminRepository _adminRepository { get; set; }

        public CheckAuthDataUseCase(IAdminRepository adminRepository, IPresenter<LoginResponse> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
        }

        public override async Task<IPresenter<LoginResponse>> HandleAsync(LoginRequest request)
        {
            var password = HashEncryption.PasswordEncode(request.Password);
            var admin = await _adminRepository.GetByCredentialInfoAsync(request.Username, password);

            if (admin != null && admin.IsApproved)
                _presenter.PresenterSuccess(new LoginResponse(admin.Token, admin.FirstName, admin.LastName, admin.IsSuperAdmin));
            else if (admin != null && !admin.IsApproved)
                _presenter.PresenterFail("Your account is not approved by admin!");
            else
                _presenter.PresenterFail("Username or Password is incorrect!");

            return _presenter;
        }
    }
}
