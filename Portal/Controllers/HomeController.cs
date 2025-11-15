using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.Models;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        private readonly HttpClient _httpClient;
        Uri _url = new Uri("http://localhost:5155/Login");
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
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModels model)
        {
            try
            {
                async Task<HttpResponseMessage> PostAsync(string endpoint, object body)
                {
                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync($"{_httpClient.BaseAddress}{endpoint}", content);
                }

                var licenseResponse = await PostAsync("Login", model);
                string data = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, model.Email),
                            new Claim(ClaimTypes.Role, model.Role)
                        };

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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}