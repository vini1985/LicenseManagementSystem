using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Portal.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        private readonly HttpClient _httpClient;
        Uri _url = new Uri("http://localhost:5155/Login/");
        public HomeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
        }
        // This action renders the login view.
        public IActionResult Index()
        {
            return View();
        }
        // This action handles user login by sending credentials to an external API and managing authentication cookies.
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserViewModels model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress+"Login", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    // Read body as string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to an object (example for token)
                    var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Access token
                    string token = result.Token;


                    // //Save token in session / cookie / your logic
                    var claims = new List<Claim>
                        {
                             new Claim(ClaimTypes.Email, model.Email),
                             new Claim(ClaimTypes.Role, result.Role),
                             new Claim("jwttoken", result.Token)
                        };
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index","License");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }
        // This action handles user logout by clearing authentication cookies.
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}Logout", null);
                if (response.IsSuccessStatusCode)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle unsuccessful response
                    return BadRequest("Logout failed.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is implemented)
                return StatusCode(500, "An error occurred during logout.");
            }
        }
    }
}