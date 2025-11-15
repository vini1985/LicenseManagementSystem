using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class LicenseViewModels
    {
        public Guid LicenseId { get; set; }
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [DisplayName("License Number")]
        public string LicenseNumber { get; set; } = string.Empty;
        [Required]
        [DisplayName("License Type")]
        public string LicenseType { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        [DisplayName("Application Date")]
        public DateTime ApplicationDate { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;
        [Required]
        public string Notes { get; set; } = string.Empty;
        [Required]
        [DisplayName("Document Type")]
        public string DocumentType { get; set; } = string.Empty;
        [Required]
        [DisplayName("Document Name")]
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        [Required]
        public IFormFile DocumentInfo { set; get; }
        public DateTime UploadedAt { get; set; }
        
        //[Required]
        //[DisplayName("License Fee")]
        //public decimal Amount { get; set; }
        //public DateTime PaymentDate { get; set; }
        //public string PaymentMethod { get; set; } = string.Empty;
    }
}
