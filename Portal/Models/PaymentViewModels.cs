using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class PaymentViewModels
    {
        public Guid LicenseId { get; set; }
        [Required]
        [DisplayName("License Fee")]
        public decimal Amount { get; set; }
        [Required]
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }
        [Required]
        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
