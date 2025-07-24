using ByteWriter.Models;
using System.Linq.Expressions;

namespace ByteWriter.Services.Interfaces
{
    public interface IArticleService
    {
        IQueryable<Article> FindAll();
        IQueryable<Article> FindByCondition(Expression<Func<Article, bool>> expression);
        void Create(Article entity);
        void Update(Article entity);
        void Delete(Article entity);
        void Save();
        void SaveAsync();
        Task<List<Article>> FindByUserIdAsync(string userId);
        Task<List<Article>> FindByBlogIdAsync(int blogId);
    }
}
