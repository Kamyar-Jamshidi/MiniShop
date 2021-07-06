using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Model.DTO
{
    public class CategoryFormVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }
}
