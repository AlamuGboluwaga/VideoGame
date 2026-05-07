namespace VideoGame.Models.Domian
{
    public class VideoGames
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
    }
}
