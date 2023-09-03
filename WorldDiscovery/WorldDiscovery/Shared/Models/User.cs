using System.Text.Json.Serialization;

namespace WorldDiscovery.Shared
{
    public class User
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("join_date")]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        [JsonPropertyName("uploaded_places")]
        public List<Place> UploadedPlaces { get; set; } = new();
    }
}
