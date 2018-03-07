using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeersForAyuda.Models
{  
    public class Available
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Labels
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }

    public class Beer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameDisplay")]
        public string NameDisplay { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("abv")]
        public string Abv { get; set; }

        [JsonProperty("styleId")]
        public int StyleId { get; set; }

        [JsonProperty("isOrganic")]
        public string IsOrganic { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusDisplay")]
        public string StatusDisplay { get; set; }

        [JsonProperty("createDate")]
        public string CreateDate { get; set; }

        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }

        [JsonProperty("style")]
        public Style Style { get; set; }

        [JsonProperty("availableId")]
        public int? AvailableId { get; set; }

        [JsonProperty("available")]
        public Available Available { get; set; }

        [JsonProperty("labels")]
        public Labels ImgLabels { get; set; }

        [JsonProperty("originalGravity")]
        public string OriginalGravity { get; set; }

        [JsonProperty("servingTemperature")]
        public string ServingTemperature { get; set; }

        [JsonProperty("servingTemperatureDisplay")]
        public string ServingTemperatureDisplay { get; set; }
    }

    public abstract class BeerJsonData
    {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("numberOfPages")]
        public int NumberOfPages { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class BeersJson : BeerJsonData
    {
        [JsonProperty("data")]
        public List<Beer> Data { get; set; }
    }

    public class BeerJson : BeerJsonData
    {
        [JsonProperty("data")]
        public Beer Data { get; set; }
    }
}