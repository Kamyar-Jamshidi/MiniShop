namespace MiniShop.Core.DTO.Request
{
    public class LikeDislikeProductRequest
    {
        public LikeDislikeProductRequest(int productId, bool like)
        {
            ProductId = productId;
            Like = like;
        }

        public int ProductId { get; set; }
        public bool Like { get; set; }
    }
}
