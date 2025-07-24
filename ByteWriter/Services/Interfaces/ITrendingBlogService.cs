using ByteWriter.Models;
using System.Linq.Expressions;

namespace ByteWriter.Services.Interfaces
{
    public interface ITrendingBlogService
    {
        IQueryable<TrendingBlog> FindAll();
        IQueryable<TrendingBlog> FindByCondition(Expression<Func<TrendingBlog, bool>> expression);
        void Create(TrendingBlog entity);
        void Update(TrendingBlog entity);
        void Delete(TrendingBlog entity);
        void Save();
        void SaveAsync();

        IEnumerable<TrendingBlog> GetTrendingBlogs();
        void AddTrendingBlog(int blogId);
        void RemoveTrendingBlog(int blogId);
        IEnumerable<Blog> GetAllBlogs();
    }
}