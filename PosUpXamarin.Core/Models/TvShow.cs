using System;
using Newtonsoft.Json;


namespace PosUpXamarin.Core.Models
{
    public class TvShow
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
    }
}