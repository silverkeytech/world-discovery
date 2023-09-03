using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorldDiscovery.Shared;
using EdgeDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WorldDiscovery.Shared.ViewModels;

namespace WorldDiscovery.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var user = JsonSerializer.Deserialize<LoginInput>(requestBody);

            if (user != null)
            {
                if (string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest();
                }

                var query = $@"SELECT User {{ first_name, last_name, email, password, join_date }} FILTER .email = <str>$email;";
                var foundUser = await _client.QueryAsync<User>(query, new Dictionary<string, object?>
                {
                    {"email", user.Email},
                });

                if (foundUser.Count() > 0 )
                {
                    var passwordHasher = new PasswordHasher<string>();
                    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, foundUser.First()?.Password ?? string.Empty, user.Password);

                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, foundUser.First()?.FirstName ?? string.Empty)
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme
                        );

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity)
                        );

                        return Ok();
                    }
                } 

                return Unauthorized();
            }

            return BadRequest();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp()
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            var newUser = JsonSerializer.Deserialize<User>(requestBody);

            if (newUser != null)
            {
                if (string.IsNullOrEmpty(newUser.FirstName)
                || string.IsNullOrEmpty(newUser.LastName)
                || string.IsNullOrEmpty(newUser.Email)
                || string.IsNullOrEmpty(newUser.Password))
                {
                    return BadRequest();
                }

                var query = "INSERT User {first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, password := <str>$password, join_date := <datetime>$join_date}";
                var passwordHasher = new PasswordHasher<string>();
                string hashedPassword = passwordHasher.HashPassword(null, newUser.Password);

                await _client.ExecuteAsync(query, new Dictionary<string, object?>
                {
                    {"first_name", newUser.FirstName},
                    {"last_name", newUser.LastName},
                    {"email", newUser.Email},
                    {"password", hashedPassword},
                    {"join_date", newUser.JoinDate}
                });

                return Ok();
            }

            return Unauthorized();
        }
    }
}
