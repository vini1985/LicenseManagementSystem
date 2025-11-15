namespace Portal.Models
{
    public class EmailViewModels
    {
        public Guid LicenseId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public string SenderAddress { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
