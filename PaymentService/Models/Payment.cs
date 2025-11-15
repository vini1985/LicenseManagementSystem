using System.ComponentModel.DataAnnotations;

namespace PaymentService.Models
{
    // Payment entity representing a payment record
    /// with properties for Id, LicenseId, Amount, PaymentDate, PaymentMethod, and Status.
    /// And to create the corresponding database table using Entity Framework Core.
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
