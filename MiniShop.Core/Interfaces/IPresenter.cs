namespace MiniShop.Core.Interfaces
{
    public interface IPresenter<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        void PresenterFail(string errorMessage);

        void PresenterSuccess(T data);
    }
}
