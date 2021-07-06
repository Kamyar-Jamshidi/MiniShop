using MiniShop.Core.Domain;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public class CategorySaveUseCase : BaseUseCase<CategorySaveRequest, bool>
    {
        protected IProductCategoryRepository _productCategoryRepository { get; set; }

        public CategorySaveUseCase(IProductCategoryRepository productCategoryRepository, IPresenter<bool> presenter)
            : base(presenter)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public override async Task<IPresenter<bool>> HandleAsync(CategorySaveRequest request)
        {
            var category = new ProductCategory();

            if (request.Id == 0)
            {
                category = new ProductCategory()
                {
                    CreateDate = DateTime.Now,
                    Title = request.Title
                };

                await _productCategoryRepository.CreateAsync(category);
            }
            else
            {
                category = await _productCategoryRepository.GetByIdAsync(request.Id);
                category.Title = request.Title;

                await _productCategoryRepository.EditAsync(category);
            }

            _presenter.PresenterSuccess(true);
            return _presenter;
        }
    }
}
