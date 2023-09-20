using Newtonsoft.Json;

namespace AuthentikSharp
{
    public class Pagination<TResult>
    {
        [JsonProperty("pagination")]
        public PaginationInfo Info { get; set; }

        [JsonProperty("results")]
        public IReadOnlyCollection<TResult> Results { get; set; } = Array.Empty<TResult>();
    }
    public class PaginationInfo
    {
        [JsonProperty("next")]
        public int NextPage { get; set; }

        [JsonProperty("previous")]
        public int PreviousPage { get; set; }

        [JsonProperty("current")]
        public int CurrentPage { get; set; }

        [JsonProperty("count")]
        public long ResultsCount { get; set; }

        [JsonProperty("total_page")]
        public int TotalPages { get; set; }
    }
}
