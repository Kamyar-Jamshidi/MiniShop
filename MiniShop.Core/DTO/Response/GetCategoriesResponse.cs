namespace MiniShop.Core.DTO.Response
{
    public class GetCategoriesResponse
    {
        public GetCategoriesResponse(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
