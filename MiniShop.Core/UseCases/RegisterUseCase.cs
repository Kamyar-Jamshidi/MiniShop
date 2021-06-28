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
    public class RegisterUseCase : BaseUseCase<RegisterRequest, bool>
    {
        protected IAdminRepository _adminRepository { get; set; }

        public RegisterUseCase(IAdminRepository adminRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(RegisterRequest request)
        {
            var password = HashEncryption.PasswordEncode(request.Password);
            var admin = await _adminRepository.GetByUsernameAsync(request.Username);

            if (admin != null)
            {
                _presenter.PresenterFail("Username is already taken!");
                return _presenter;
            }

            admin = new Admin()
            {
                CreateDate = DateTime.Now,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = password,
                IsApproved = false,
                IsSuperAdmin = false,
                Token = Guid.NewGuid().ToString().Replace("-", string.Empty)
            };

            await _adminRepository.CreateAsync(admin);

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
