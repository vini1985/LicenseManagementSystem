using System.ComponentModel.DataAnnotations;

namespace DocumentService.Models
{
    // Documents entity representing a document record
    /// with properties for DocumentId, LicenseId, DocumentType, DocumentName, DocumentPath, and UploadedAt.
    /// And to create the corresponding database table using Entity Framework Core.
    public class Documents
    {
        [Key]
        public Guid DocumentId { get; set; }
        public Guid LicenseId { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }
}
