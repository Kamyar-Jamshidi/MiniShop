namespace MiniShop.Core.DTO.Request
{
    public class ChangeProductStatusRequest
    {
        public ChangeProductStatusRequest(string token, int productId)
        {
            Token = token;
            ProductId = productId;
        }

        public string Token { get; set; }
        public int ProductId { get; set; }
    }
}
