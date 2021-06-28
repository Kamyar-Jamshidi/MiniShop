using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class GetAllUsersUseCase : BaseUseCase<string, List<GetAllUsersReponse>>
    {
        protected IAdminRepository _adminRepository { get; set; }

        public GetAllUsersUseCase(IAdminRepository adminRepository, IPresenter<List<GetAllUsersReponse>> presenter)
            : base(presenter)
        {
            _adminRepository = adminRepository;
        }

        public override async Task<IPresenter<List<GetAllUsersReponse>>> HandleAsync(string request)
        {
            var admin = await _adminRepository.GetByTokenAsync(request);

            if (admin == null || !admin.IsSuperAdmin)
            {
                _presenter.PresenterFail("You cant access this section!");
                return _presenter;
            }

            var list = await _adminRepository.GetAllAsync();

            _presenter.PresenterSuccess(list.Select(x => new GetAllUsersReponse(x.Token, x.FirstName, x.LastName, x.Username, x.IsApproved)).ToList());
            return _presenter;
        }
    }
}
