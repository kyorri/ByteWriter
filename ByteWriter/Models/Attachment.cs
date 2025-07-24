namespace ByteWriter.Models
{
    public class Attachment
    {
        public int AttachmentID { get; set; }
        public string AttachmentName { get; set; }
        public byte[] AttachmentContent { get; set; }

        public int? ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
