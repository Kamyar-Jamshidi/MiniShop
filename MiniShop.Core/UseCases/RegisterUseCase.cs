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
        protected IUserRepository _adminRepository { get; set; }

        public RegisterUseCase(IUserRepository adminRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(RegisterRequest request)
        {
            var admin = await _adminRepository.GetByUsernameAsync(request.Username);

            if (admin != null)
            {
                _presenter.PresenterFail("Username is already taken!");
                return _presenter;
            }

            admin = new User()
            {
                CreateDate = DateTime.Now,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
                IsApproved = false,
                IsAdmin = false
            };

            await _adminRepository.CreateAsync(admin);

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
