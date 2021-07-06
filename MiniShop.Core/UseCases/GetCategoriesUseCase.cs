using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class GetCategoriesUseCase : BaseUseCase<string, List<GetCategoriesResponse>>
    {
        protected IProductCategoryRepository _productCategoryRepository { get; set; }

        public GetCategoriesUseCase(IProductCategoryRepository productCategoryRepository, IPresenter<List<GetCategoriesResponse>> presenter)
            : base(presenter)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public override async Task<IPresenter<List<GetCategoriesResponse>>> HandleAsync(string request = null)
        {
            var list = await _productCategoryRepository.GetAllAsync();

            _presenter.PresenterSuccess(list.Select(x => new GetCategoriesResponse(x.Id, x.Title, x.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"))).ToList());

            return _presenter;
        }
    }
}
