using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public Guid LicenseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
