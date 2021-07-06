using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Model.DTO
{
    public class LoginVM
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

    }
}
