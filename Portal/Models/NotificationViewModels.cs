using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class NotificationViewModels
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        [Required]
        [DisplayName("Sender Address")]
        public string SenderAddress { get; set; } = string.Empty;
        [Required]
        [DisplayName("Email Content")]
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
