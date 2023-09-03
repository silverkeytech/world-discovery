using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared
{
    public class LabelCategory
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("background")]
        public string Background { get; set; } = string.Empty;

        [JsonPropertyName("font_color")]
        public string FontColor { get; set; } = string.Empty;
    }
}
