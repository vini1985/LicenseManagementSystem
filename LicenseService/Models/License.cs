using System.ComponentModel.DataAnnotations;

namespace LicenseService.Models
{
    // License entity representing a license record
    /// with properties for LicenseId, TenantId, UserId, LicenseNumber, LicenseType, Category, ApplicationDate, Status, and Notes.
    /// And to create the corresponding database table using Entity Framework Core.
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
