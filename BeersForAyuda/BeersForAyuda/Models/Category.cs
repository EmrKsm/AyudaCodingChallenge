using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeersForAyuda.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("createDate")]
        public string CreateDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }

        public List<Style> Styles { get; set; }
    }

    public class CategoriesJson : JsonData
    {
        [JsonProperty("data")]
        public List<Category> Data { get; set; }
    }
    public class CategoryJson : JsonData
    {
        [JsonProperty("data")]
        public Category Data { get; set; }
    }
}