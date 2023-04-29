using Albayan_Task.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        T GetByIdIncludeAsync(long id, string includeProperties = "", bool tracking = false);

        Task<T> GeTWithSpec(ISpecifications<T> specification);
        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specification);
        Task<int> CountAsync(ISpecifications<T> specifications);
        void DeleteAsync(T entity);
        void UpdateAsync(T entity);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Contains(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);
        void AddRange(IEnumerable<T> entities);
        Task< IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int pagesize = 1, int pagenum = 50, bool tracking = false);
    }
}
