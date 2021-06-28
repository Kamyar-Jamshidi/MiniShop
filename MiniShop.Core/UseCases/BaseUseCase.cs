using MiniShop.Core.Interfaces;
using MiniShop.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MiniShop.Core.UseCases
{
    public abstract class BaseUseCase<TRequest, TResponse>
    {
        protected IPresenter<TResponse> _presenter { get; set; }

        protected BaseUseCase(IPresenter<TResponse> presenter)
        {
            _presenter = presenter;
        }

        public abstract Task<IPresenter<TResponse>> HandleAsync(TRequest request = default);
    }
}
