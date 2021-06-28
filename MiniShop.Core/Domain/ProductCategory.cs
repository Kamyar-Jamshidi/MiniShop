using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MiniShop.Core.Domain
{
    public class ProductCategory : BaseDomain
    {
        public string Title { get; set; }

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}
