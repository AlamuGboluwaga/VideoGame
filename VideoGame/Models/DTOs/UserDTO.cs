using System.ComponentModel.DataAnnotations;

namespace VideoGame.Models.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
