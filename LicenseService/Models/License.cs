using System.ComponentModel.DataAnnotations;

namespace LicenseService.Models
{
    public class License
    {
        [Key]
        public Guid LicenseId { get; set; }
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string LicenseType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

    }
}
