using Newtonsoft.Json;

namespace BeersForAyuda.Models
{
    public abstract class JsonData
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}