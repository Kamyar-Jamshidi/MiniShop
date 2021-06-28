using MiniShop.Core.Interfaces;

namespace MiniShop.Web.Present
{
    public class Presenter<T> : IPresenter<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public void PresenterFail(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Status = false;
        }

        public void PresenterSuccess(T data)
        {
            Status = true;
            Data = data;
            ErrorMessage = string.Empty;
        }
    }
}
