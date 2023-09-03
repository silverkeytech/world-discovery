using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared
{
    public class Address
    {
        [JsonPropertyName("street_number")]
        public int StreetNumber { get; set; }

        [JsonPropertyName("street_name")]
        public string StreetName { get; set; } = string.Empty;

        [JsonPropertyName("neighbourhood")]
        public string Neighbourhood { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("google_map")]
        public string GoogleMap { get; set; } = string.Empty;
    }
}
