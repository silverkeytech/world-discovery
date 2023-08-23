using EdgeDB;
using Microsoft.AspNetCore.Mvc;
using WorldDiscovery.Shared;

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

        [HttpGet("get-places")]
        public async Task<IActionResult> GetAllPlaces()
        {
            Console.WriteLine($"------- From the response -------");

            var places = await _client.QueryAsync<Place>("SELECT Place {*};");

            if (places != null)
            {
                return Ok(places.ToList());
            }

            return BadRequest();
        }

        [HttpGet("get-label-categories")]
        public async Task<IActionResult> GetAllLabelCategories()
        {
            var labelCategories = await _client.QueryAsync<LabelCategory>("SELECT LabelCategory {*};");

            if (labelCategories != null)
            {
                return Ok(labelCategories.ToList());
            }

            return BadRequest();
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
                        labels := <array<str>>$labels,
                        facebook_link := <str>$facebook_link,
                        website_link := <str>$website_link,
                        email := <str>$email,
                        phone_number := <str>$phone_number,
                        address := <jsonb>$address,
                        place_image := <jsonb>$place_image,
                        sections := <array<str>>$sections,
                        created_by := <str>$created_by,
                        last_updated := <datetime>$last_updated,
                    };
                ", new
                {
                    name = place.Name,
                    category = place.Category,
                    description = place.Description,
                    labels = place.Labels,
                    facebook_link = place.FacebookLink,
                    website_link = place.WebsiteLink,
                    email = place.Email,
                    phone_number = place.PhoneNumber,
                    address = place.Address,
                    place_image = place.PlaceImage, 
                    sections = place.Sections,
                    created_by = place.CreatedBy,
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
