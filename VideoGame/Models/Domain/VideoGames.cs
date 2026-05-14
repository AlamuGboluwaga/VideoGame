using System.ComponentModel.DataAnnotations;

namespace VideoGame.Models.Domain
{
    public class VideoGames
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 3)]
        public string Platform { get; set; } = string.Empty;
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 3)]
        public string Developer { get; set; } = string.Empty;
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 3)]
        public string Publisher { get; set; } = string.Empty;

    }
}
