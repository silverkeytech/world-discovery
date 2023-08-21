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
    }
}
