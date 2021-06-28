namespace MiniShop.Core.DTO.Response
{
    public class GetProductsResponse
    {
        public GetProductsResponse(int id, string title, string categoryTitle,
            int likes, bool isTopRate, bool isApproved, string createOn,
            string description, int categoryId)
        {
            Id = id;
            Title = title;
            CategoryTitle = categoryTitle;
            Likes = likes;
            IsTopRate = isTopRate;
            IsApproved = isApproved;
            CreateOn = createOn;
            Description = description;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }
        public int Likes { get; set; }
        public bool IsTopRate { get; set; }
        public bool IsApproved { get; set; }
        public string CreateOn { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
