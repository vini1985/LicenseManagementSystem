using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    // Users entity representing a user record
    /// with properties for UserId, UserName, UserEmail, and UserPassword.
    /// And to create the corresponding database table using Entity Framework Core.
    public class Users 
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserEmail { get; set;} = string.Empty;
        [Required]
        public string UserPassword { get; set;} = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
