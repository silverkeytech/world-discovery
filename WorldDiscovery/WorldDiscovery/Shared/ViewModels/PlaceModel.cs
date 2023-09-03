using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared.ViewModels
{
    public class PlaceModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("place_image")]
        public Image PlaceImage { get; set; } = new();
    }
}
