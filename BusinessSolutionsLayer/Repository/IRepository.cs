using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessSolutionsLayer.Repository
{
    public interface IRepository<T>
    {
        void Add(T obj);

        Task<int> AddRangeAsync(IEnumerable<T> objCollection);

        IEnumerable<T> Get(Expression<Func<T, bool>> func);

        IEnumerable<T> Get<TProperty>(Expression<Func<T, bool>> func, params Expression<Func<T, TProperty>>[] includes);

        void Update(T obj);

        void SqlInject(string qurey);

        void Delete(T obj);
    }
}
