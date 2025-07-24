namespace ByteWriter.Models
{
    public class TrendingBlog
    {
        public int TrendingBlogID { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
