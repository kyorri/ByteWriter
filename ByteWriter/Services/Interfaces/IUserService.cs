using ByteWriter.Models;
using System.Linq.Expressions;

namespace ByteWriter.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<User> FindAll();
        IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression);
        void Create(User entity);
        void Update(User entity);
        void Delete(User entity);
        void Save();
        void SaveAsync();

    }
}
