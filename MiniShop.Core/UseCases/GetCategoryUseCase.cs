using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class GetCategoryUseCase : BaseUseCase<int, string>
    {
        protected IProductCategoryRepository _productCategoryRepository { get; set; }

        public GetCategoryUseCase(IProductCategoryRepository productCategoryRepository, IPresenter<string> presenter)
            : base(presenter)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public override async Task<IPresenter<string>> HandleAsync(int request)
        {
            var category = await _productCategoryRepository.GetByIdAsync(request);
            _presenter.PresenterSuccess(category.Title);
            return _presenter;
        }
    }
}
