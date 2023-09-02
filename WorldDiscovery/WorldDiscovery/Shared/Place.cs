using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared
{
    public class Place
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("labels")]
        public List<Label> Labels { get; set; } = new();

        [JsonPropertyName("facebook_link")]
        public string FacebookLink { get; set; } = string.Empty;

        [JsonPropertyName("website_link")]
        public string WebsiteLink { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public Address Address { get; set; } = new();

        [JsonPropertyName("place_image")]
        public Image PlaceImage { get; set; } = new();

        [JsonPropertyName("sections")]
        public List<Section> Sections { get; set; } = new();

        [JsonPropertyName("created_by")]
        public User CreatedBy { get; set; } = new();

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
