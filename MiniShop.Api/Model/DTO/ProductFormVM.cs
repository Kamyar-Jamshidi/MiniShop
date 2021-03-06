using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Model.DTO
{
    public class ProductFormVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsTopRate { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
