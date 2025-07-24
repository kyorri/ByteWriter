using ByteWriter.Models;
using ByteWriter.Repositories;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace ByteWriter.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ArticleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IQueryable<Article> FindAll()
        {
            return _repositoryWrapper.ArticleRepository.FindAll();
        }
        public IQueryable<Article> FindByCondition(Expression<Func<Article, bool>> expression)
        {
            return _repositoryWrapper.ArticleRepository.FindByCondition(expression);
        }
        public void Create(Article entity)
        {
            _repositoryWrapper.ArticleRepository.Create(entity);
        }
        public void Update(Article entity)
        {
            _repositoryWrapper.ArticleRepository.Update(entity);
        }
        public void Delete(Article entity)
        {
            _repositoryWrapper.ArticleRepository.Delete(entity);
        }

        public void Save()
        {
            _repositoryWrapper.Save();
        }

        public void SaveAsync()
        {
            _repositoryWrapper.SaveAsync();
        }
        public async Task<List<Article>> FindByUserIdAsync(string userId)
        {
            var userWithArticles = await _repositoryWrapper.UserRepository
                                                            .FindByCondition(u => u.Id == userId)
                                                            .Include(u => u.Blog)
                                                            .ThenInclude(b => b.Articles)
                                                            .FirstOrDefaultAsync();

            return userWithArticles?.Blog?.Articles.ToList() ?? new List<Article>();
        }
        public async Task<List<Article>> FindByBlogIdAsync(int blogId)
        {
            return await _repositoryWrapper.ArticleRepository.FindByCondition(a => a.BlogId == blogId).ToListAsync();
        }
    }
}
