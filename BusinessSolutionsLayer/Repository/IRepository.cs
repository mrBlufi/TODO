using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BusinessSolutionsLayer.Repository
{
    public interface IRepository<T>
    {
        T Add(T obj);
        IEnumerable<T> Get(Expression<Func<T, bool>> func);

        IEnumerable<T> Get<TProperty>(Expression<Func<T, bool>> func, params Expression<Func<T, TProperty>>[] includes);
        T Update(T obj);
        void Delete(T obj);
    }
}
