namespace MiniShop.Core.DTO.Response
{
    public class GetCategoriesResponse
    {
        public GetCategoriesResponse(int id, string title, string createOn)
        {
            Id = id;
            Title = title;
            CreateOn = createOn;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string CreateOn { get; set; }
    }
}
