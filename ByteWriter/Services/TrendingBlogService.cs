using ByteWriter.Models;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteWriter.Services
{
    public class TrendingBlogService : ITrendingBlogService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        

        public TrendingBlogService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IQueryable<TrendingBlog> FindAll()
        {
            return _repositoryWrapper.TrendingBlogRepository.FindAll();
        }
        public IQueryable<TrendingBlog> FindByCondition(Expression<Func<TrendingBlog, bool>> expression)
        {
            return _repositoryWrapper.TrendingBlogRepository.FindByCondition(expression);
        }
        public void Create(TrendingBlog entity)
        {
            _repositoryWrapper.TrendingBlogRepository.Create(entity);
        }
        public void Update(TrendingBlog entity)
        {
            _repositoryWrapper.TrendingBlogRepository.Update(entity);
        }
        public void Delete(TrendingBlog entity)
        {
            _repositoryWrapper.TrendingBlogRepository.Delete(entity);
        }

        public void Save()
        {
            _repositoryWrapper.Save();
        }

        public void SaveAsync()
        {
            _repositoryWrapper.SaveAsync();
        }

        public IEnumerable<TrendingBlog> GetTrendingBlogs()
        {
            return _repositoryWrapper.TrendingBlogRepository.FindAll().ToList();
        }

        public void AddTrendingBlog(int blogId)
        {
            var trendingBlog = new TrendingBlog { BlogId = blogId };
            _repositoryWrapper.TrendingBlogRepository.Create(trendingBlog);
            _repositoryWrapper.Save();
        }

        public void RemoveTrendingBlog(int blogId)
        {
            var trendingBlog = _repositoryWrapper.TrendingBlogRepository.FindByCondition(t => t.BlogId == blogId).FirstOrDefault();
            if (trendingBlog != null)
            {
                _repositoryWrapper.TrendingBlogRepository.Delete(trendingBlog);
                _repositoryWrapper.Save();
            }
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            return _repositoryWrapper.BlogRepository.FindAll().ToList();
        }
    }
}
