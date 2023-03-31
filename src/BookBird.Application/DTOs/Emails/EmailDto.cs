namespace BookBird.Application.DTOs.Emails
{
    public class EmailDto
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string PlainContent { get; set; }
        public EmailAttachmentDto Attachment { get; set; } = null;
    }

    public class EmailAttachmentDto
    {
        public byte[] Content { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public string Disposition { get; set; }
        public string ContentId { get; set; }
    }
}