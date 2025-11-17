using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class UserViewModels
    {
        public string UserName { get; set; }= string.Empty;
        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }= string.Empty;
        [Required]
        public string Password { get; set; }= string.Empty;
        public string Role { get; set; }= "User"; 
    }
}
