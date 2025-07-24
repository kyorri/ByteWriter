using ByteWriter.Models;
using System.Linq.Expressions;

namespace ByteWriter.Services.Interfaces
{
    public interface IBlogService
    {
        IQueryable<Blog> FindAll();
        IQueryable<Blog> FindByCondition(Expression<Func<Blog, bool>> expression);
        void Create(Blog entity);
        void Update(Blog entity);
        void Delete(Blog entity);

        void Save();
        void SaveAsync();

    }
}
