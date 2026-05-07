namespace VideoGame.Models.DTOs
{
    public class VideoGamesDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Platform { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }

    }
}
