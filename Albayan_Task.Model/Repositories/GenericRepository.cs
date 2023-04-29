using Albayan_Task.Domain.Entities;
using Albayan_Task.Domain.Interfaces;
using Albayan_Task.Domain.Specifications;
using Albayan_Task.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Model.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MyDbContext _storeContext;

        public GenericRepository(MyDbContext storeContext)
        {
            _storeContext = storeContext;
        }
        public void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _storeContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdAsync(long id)
        {
            try
            {
                return await _storeContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// not implimented
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAsync(T entity)
        {

        }
        //Specification Pattern
        public async Task<T> GetEntityWithSpec(ISpecifications<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }


        public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        {
            return SpecificationEvaluatOr<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), specifications);
        }

        public void Add(T entity)
        {
            _storeContext.Add<T>(entity);
        }

        public void Update(T entity)
        {
            var xx = _storeContext.Attach<T>(entity);
            _storeContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _storeContext.Set<T>().Remove(entity);
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate) > 0 ? true : false;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _storeContext.Set<T>().Where(predicate).Count();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _storeContext.Set<T>().AddRange(entities);
        }

        public Task<T> GeTWithSpec(ISpecifications<T> specification)
        {
            throw new NotImplementedException();
        }

        public T GetByIdIncludeAsync(long id, string includeProperties = "",bool tracking = false)
        {
            IQueryable<T> query = _storeContext.Set<T>();
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if(!tracking)
                return query.FirstOrDefault(s=>s.Id ==id);
            else
                return query.AsNoTracking().FirstOrDefault(s => s.Id == id);
        }

        public async Task< IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int pagenum = 1, int pagesize = 50,bool tracking =false)
        {
            IQueryable<T> query = _storeContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (pagesize > 0)
            {
                if (pagesize > 50)
                    pagesize = 50;
                if (pagenum < 1)
                    pagenum = 1;
                query.Skip((pagenum - 1) * 50).Take(pagesize);
            }

            if (orderBy != null)
            {
                if(!tracking)
                    return await orderBy(query).ToListAsync();
                else
                    return await orderBy(query).AsNoTracking().ToListAsync();
            }
            else
            {
                if (!tracking)
                    return await query.ToListAsync();
                else
                    return await query.AsNoTracking().ToListAsync();
            }
        }

       
    }
}
