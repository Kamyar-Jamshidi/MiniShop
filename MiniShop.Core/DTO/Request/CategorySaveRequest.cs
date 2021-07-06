namespace MiniShop.Core.DTO.Request
{
    public class CategorySaveRequest
    {
        public CategorySaveRequest(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
