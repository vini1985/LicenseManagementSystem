using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Notifications
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        [Required]
        public string SenderAddress { get; set; } = string.Empty;
        [Required]
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
