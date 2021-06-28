using System.Text.Json.Serialization;

namespace MiniShop.Core.Domain
{
    public class Product : BaseDomain
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public bool IsTopRate { get; set; }
        public bool IsApproved { get; set; }

        public int ProductCategoryId { get; set; }

        [JsonIgnore]
        public ProductCategory ProductCategory { get; set; }
    }
}
