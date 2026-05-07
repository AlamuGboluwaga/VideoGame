namespace VideoGame.Models.DTOs
{
    public class VideoGamesDTO
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

    }
}
