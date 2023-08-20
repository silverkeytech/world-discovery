using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorldDiscovery.Shared;
using EdgeDB;

namespace WorldDiscovery.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly EdgeDBClient _client;

        public AuthController(EdgeDBClient client)
        {
            _client = client;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            var newUser = JsonSerializer.Deserialize<LoginInput>(requestBody);

            if (newUser != null)
            {
                if (string.IsNullOrEmpty(newUser.Email)
                || string.IsNullOrEmpty(newUser.Password))
                {
                    return BadRequest();
                }

                //var query = $@"SELECT Contact {{ email_address, password }} FILTER .username = <str>email_address and .password = <str>password;";
                //var users = await _client.QueryAsync<LoginInput>(query, new Dictionary<string, object?>
                //{
                //    {"email", newUser.Email},
                //    {"password", newUser.Password},
                //});

                Console.WriteLine($"-------- Login Credentials are {newUser.Email}, {newUser.Password}");

                var users = "";

                if (users.Count() > 0 )
                {
                    return Ok();
                } 
                else
                {
                    return Unauthorized();
                }
            }

            return BadRequest();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp()
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            var newUser = JsonSerializer.Deserialize<SignUpInput>(requestBody);

            if (newUser != null)
            {
                if (string.IsNullOrEmpty(newUser.FirstName)
                || string.IsNullOrEmpty(newUser.LastName)
                || string.IsNullOrEmpty(newUser.Email)
                || string.IsNullOrEmpty(newUser.Password))
                {
                    return BadRequest();
                }

                Console.WriteLine($"-------- Login Credentials are {newUser.Email}, {newUser.Password}");

                return Ok();
            }

            return Unauthorized();
        }
    }
}
