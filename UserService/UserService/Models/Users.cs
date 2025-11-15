using System.ComponentModel.DataAnnotations;

namespace blog.api.Models
{
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
