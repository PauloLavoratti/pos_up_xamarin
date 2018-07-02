using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PosUpXamarin.Core.Models
{
    public class TvShowDetail
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "original_name")]
        public string OriginalName { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public double Popularity { get; set; }

        [JsonProperty(PropertyName = "first_air_date")]
        public DateTimeOffset? FirstAirDate { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }

        [JsonIgnore]
        public string GenresNames { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "number_of_episodes")]
        public int NumberOfEpisodes { get; set; }

        [JsonProperty(PropertyName = "number_of_seasons")]
        public int NumberOfSeasons { get; set; }
    }
}
