using System.ComponentModel.DataAnnotations;

namespace blog.api.Models
{
    // Users entity representing a user record
    /// with properties for UserId, UserName, UserEmail, and UserPassword.
    /// And to create the corresponding database table using Entity Framework Core.
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }=string.Empty;
        [Required]
        public string UserEmail { get; set;} = string.Empty;
        [Required]
        public string UserPassword { get; set;} = string.Empty;
    }
}
