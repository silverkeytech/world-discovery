using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared
{
    public class Label
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public LabelCategory Category { get; set; } = new();
    }
}
