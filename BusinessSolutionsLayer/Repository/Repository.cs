using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessSolutionsLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IMapper mapper;

        private readonly IApplicationContextFactory contextFactory;

        public Repository(IApplicationContextFactory contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public T Add(T obj)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Entry(obj).State = EntityState.Added;
                context.SaveChanges();
            }

            return obj;
        }

        public void Delete(T obj)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Remove(obj);
                context.SaveChanges();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> func)
        {
            using (var context = contextFactory.GetContext())
            {
                 return context.Set<T>().AsNoTracking().Where(func).ToList();
            }
        }

        public IEnumerable<T> Get<TProperty>(Expression<Func<T, bool>> func, params Expression<Func<T, TProperty>>[] includes)
        {
            using (var context = contextFactory.GetContext())
            {
                return this.MultiInclude(context.Set<T>().AsNoTracking(),includes).Where(func).ToList();
            }
        }

        public T Update(T obj)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Attach(obj).State = EntityState.Modified;
                context.SaveChanges();
            }

            return obj;
        }

        private IQueryable<T> MultiInclude<TProperty>(IQueryable<T> query, params Expression<Func<T, TProperty>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
