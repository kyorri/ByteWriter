using System.Linq.Expressions;
using ByteWriter.Repositories.Interfaces;
using ByteWriter.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteWriter.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BloggingContext BloggingContext { get; set; }

        public RepositoryBase(BloggingContext bloggingContext)
        {
            BloggingContext = bloggingContext;
        }

        public IQueryable<T> FindAll()
        {
            return BloggingContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return BloggingContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            BloggingContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            BloggingContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            BloggingContext.Set<T>().Remove(entity);
        }
    }
}
