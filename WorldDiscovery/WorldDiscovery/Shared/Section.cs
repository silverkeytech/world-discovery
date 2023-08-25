using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared
{
    public class Section
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string[] Description { get; set; } = new string[] {};
    }
}
