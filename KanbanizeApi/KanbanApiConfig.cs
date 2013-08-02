namespace KanbanNotifier.KanbanizeApi
{
    public class KanbanApiConfig
    {
        public KanbanApiConfig(string url, string key, int boardId, int resultsPerPage)
        {
            ApiURL = url;
            ApiKey = key;
            BoardId = boardId;
            ResultsPerPage = resultsPerPage;
        }

        public string ApiURL { get; private set; }
        public string ApiKey { get; private set; }
        public int BoardId { get; private set; }
        public int ResultsPerPage { get; private set; }
    }
}
