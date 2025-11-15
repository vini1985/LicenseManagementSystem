namespace Portal.Models
{
    public class DocumentViewModels
    {
        public Guid DocumentId { get; set; }
        public Guid LicenseId { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }
}
