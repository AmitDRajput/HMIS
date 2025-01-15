using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Domain.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);

        T GetById(long id);
        IEnumerable<T> GetAll();    
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);

        void Update(T entity);
        void RemoveRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
