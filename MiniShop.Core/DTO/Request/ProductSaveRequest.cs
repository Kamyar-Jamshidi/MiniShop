namespace MiniShop.Core.DTO.Request
{
    public class ProductSaveRequest
    {
        public ProductSaveRequest(string token, int id, string title, string description, int categoryId, bool isTopRate)
        {
            Token = token;
            Id = id;
            Title = title;
            Description = description;
            CategoryId = categoryId;
            IsTopRate = isTopRate;
        }

        public string Token { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsTopRate { get; set; }
    }
}
