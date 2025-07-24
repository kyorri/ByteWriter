using ByteWriter.Models;
using ByteWriter.Repositories;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace ByteWriter.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public BlogService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IQueryable<Blog> FindAll() {
            return _repositoryWrapper.BlogRepository.FindAll();
        }
        public IQueryable<Blog> FindByCondition(Expression<Func<Blog, bool>> expression) {
            return _repositoryWrapper.BlogRepository.FindByCondition(expression);
        }
        public void Create(Blog entity) {
            _repositoryWrapper.BlogRepository.Create(entity);
        }
        public void Update(Blog entity) {
            _repositoryWrapper.BlogRepository.Update(entity);
        }
        public void Delete(Blog entity) {
            _repositoryWrapper.BlogRepository.Delete(entity);
        }

        public void Save()
        {
            _repositoryWrapper.Save();
        }

        public void SaveAsync()
        {
            _repositoryWrapper.SaveAsync();
        }
    }
}
