using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using WorldDiscovery.Shared;
using WorldDiscovery.Shared.ViewModels;

namespace WorldDiscovery.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : Controller
    {
        private readonly EdgeDBClient _client;

        public PlaceController(EdgeDBClient client)
        {
            _client = client;
        }

        [HttpGet("get-place")]
        public async Task<IActionResult> GetPlaceByName(string name)
        {
            var query = $@" SELECT Place 
            {{
                name, 
                category, 
                description, 
                labels: {{
                    name,
                    category: {{
                        name,
                        background,
                        font_color
                    }}
                }}, 
                facebook_link, 
                website_link,
                email,
                phone_number,
                address: {{
                    street_number, 
                    street_name, 
                    neighbourhood, 
                    city, 
                    country, 
                    google_map
                }}, 
                place_image: {{
                    title,
                    description,
                    url
                }},
                sections: {{
                    title,
                    description
                }},
                created_by: {{
                    first_name,
                    last_name,
                }},
                last_updated
            }} 
            FILTER .name = <str>$name;";

            var place = await _client.QueryAsync<Place>(query, new Dictionary<string, object?>
            {
                { "name", name},
            });

            if (place != null)
            {
                return Ok(place.FirstOrDefault());
            }

            return BadRequest();
        }

        [HttpGet("get-places")]
        public async Task<IActionResult> GetAllPlaces()
        {
            var places = await _client.QueryAsync<PlaceModel>(@"
                SELECT Place 
                {
                    name,  
                    description, 
                    place_image: {
                        title,
                        description,
                        url
                    }
                };
            ");

            if (places != null)
            {
                return Ok(places.ToList());
            }

            return BadRequest();
        }

        [HttpGet("get-label-categories")]
        public async Task<IActionResult> GetAllLabelCategories()
        {
            try
            {
                var labelCategories = await _client.QueryAsync<LabelCategory>("SELECT LabelCategory {*};");

                if (labelCategories != null)
                {
                    return Ok(labelCategories.ToList());
                }
                else
                {
                    return NotFound("No label categories found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving label categories: {ex.Message}");
                return StatusCode(500, "Internal Server Error: Unable to retrieve label categories.");
            }
        }

        [HttpGet("get-labels")]
        public async Task<IActionResult> GetLabels()
        {
            try
            {
                var labels = await _client.QueryAsync<Shared.Label>("SELECT Label {*};");

                if (labels != null)
                {
                    return Ok(labels.ToList());
                }
                else
                {
                    return NotFound("No label categories found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving label categories: {ex.Message}");
                return StatusCode(500, "Internal Server Error: Unable to retrieve label categories.");
            }
        }


        [HttpPost("add-place")]
        public async Task<IActionResult> AddPlace([FromBody] Place place)
        {          
            try
            {
                if (place == null)
                {
                    return BadRequest("Invalid request data.");
                }

                await _client.ExecuteAsync(@"
                INSERT Place {
                    name := <str>$name,
                    category := <str>$category,
                    description := <str>$description,
                    labels := (
                        SELECT Label FILTER .name IN array_unpack(<array<str>>$labelsparameter)
                    ),
                    facebook_link := <str>$facebook_link,
                    website_link := <str>$website_link,
                    email := <str>$email,
                    phone_number := <str>$phone_number,
                    address := (INSERT Address {
                        street_number := <int32>$street_number,
                        street_name := <str>$street_name,
                        city := <str>$city,
                        country := <str>$country,
                        google_map := <str>$google_map,
                        neighbourhood := <str>$neighbourhood
                    }),
                    place_image := (INSERT Image {
                        title := <str>$image_title,
                        description := <str>$image_description,
                        url := <str>$image_url
                    }),
                    sections := (
                        FOR section IN <array<tuple<title: str, description: str>>><json>$sections
                        UNION (
                            INSERT Section {
                                title := section.title,
                                description := section.description
                            }
                        )
                    ),
                    created_by := (SELECT User FILTER .email = <str>$created_by_email LIMIT 1),
                    last_updated := <datetime>$last_updated
                };
                ", new
                {
                    name = place.Name,
                    category = place.Category,
                    description = place.Description,
                    labelsparameter = place.Labels.Select(label => label.Name).ToList(),
                    facebook_link = place.FacebookLink,
                    website_link = place.WebsiteLink,
                    email = place.Email,
                    phone_number = place.PhoneNumber,
                    street_number = place.Address.StreetNumber,
                    street_name = place.Address.StreetName,
                    city = place.Address.City,
                    country = place.Address.Country,
                    google_map = place.Address.GoogleMap,
                    neighbourhood = place.Address.Neighbourhood,
                    image_title = place.PlaceImage.Title,
                    image_description = place.PlaceImage.Description,
                    image_url = place.PlaceImage.Url,
                    sections = place.Sections.Select(section => new
                    {
                        title = section.Title,
                        description = section.Description
                    }).ToList(),
                    created_by_email = place.CreatedBy.Email,
                    last_updated = place.LastUpdated
                });
                return Ok("Place added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
