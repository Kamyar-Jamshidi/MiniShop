namespace MiniShop.Core.DTO.Request
{
    public class ProductSaveRequest
    {
        public ProductSaveRequest(int id, string title, string description, bool isTopRate, int categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            IsTopRate = isTopRate;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; internal set; }
        public bool IsTopRate { get; internal set; }
        public int CategoryId { get; internal set; }
    }
}
