 using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;

namespace ByteWriter.Repositories
{
    public class TrendingBlogRepository : RepositoryBase<TrendingBlog>, ITrendingBlogRepository
    {
        public TrendingBlogRepository(BloggingContext bloggingContext)
            : base(bloggingContext)
        {
            
        }
    }
}
