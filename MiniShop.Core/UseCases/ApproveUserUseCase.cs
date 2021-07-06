using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class ApproveUserUseCase : BaseUseCase<string, bool>
    {
        protected IUserRepository _adminRepository { get; set; }

        public ApproveUserUseCase(IUserRepository adminRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(string request)
        {
            await _adminRepository.ApproveUserAsync(request);
            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
