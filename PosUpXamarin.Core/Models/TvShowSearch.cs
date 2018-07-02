using System.Collections.Generic;
using Newtonsoft.Json;

namespace PosUpXamarin.Core.Models
{
    public class TvShowSearch
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<TvShow> TvShows { get; set; }
    }
}
