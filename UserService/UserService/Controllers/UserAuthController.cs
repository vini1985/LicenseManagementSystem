using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _jwtExpiryMinutes;
        private readonly IConfiguration _configuration;
    
        public UserAuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtKey = _configuration["Jwt:Key"].ToString();
            _jwtIssuer = _configuration["Jwt:Issuer"].ToString();
            _jwtAudience = _configuration["Jwt:Audience"].ToString();
            _jwtExpiryMinutes =int.Parse(_configuration["Jwt:ExpiryMinutes"]);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if(model == null || string.IsNullOrEmpty(model.Email) 
                || string.IsNullOrEmpty(model.Password)
                || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Invalid registration data");
            }
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return Conflict("User with this email already exists");
            }
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user =await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return Unauthorized(new  { success = false, message = "Invalid Username or Password" });
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { success = false, message = "Invalid Username or Password" });
            }
            var token = GenerateJwtToken(user);
            return Ok(new { success = true, token = token, role = "Admin" });
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return Ok("User Logged out Successfully");
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("Name",user.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds =new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: Claims,
                expires: DateTime.Now.AddMinutes(_jwtExpiryMinutes),
                signingCredentials:creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
