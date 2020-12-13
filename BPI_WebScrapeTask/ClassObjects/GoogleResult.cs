namespace BPI_WebScrapeTask.ClassObjects
{
    /// <summary>
    /// Object for storing google search engine results
    /// </summary>
    public class GoogleResult
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public int Rank { get; set; }
        public string SearchPhrase { get; set; }
    }
}
