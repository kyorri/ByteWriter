namespace ByteWriter.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public DateTime? ArticleTimestamp { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleContent { get; set; }

        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
