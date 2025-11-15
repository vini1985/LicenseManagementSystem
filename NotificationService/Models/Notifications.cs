using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    // Notifications entity representing a notification record
    /// with properties for Id, Subject, Recipient, SenderAddress, Message, and CreatedAt.
    /// And to create the corresponding database table using Entity Framework Core.
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
