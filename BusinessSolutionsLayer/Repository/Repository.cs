using AutoMapper;
using BusinessSolutionsLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessSolutionsLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IMapper mapper;
        private readonly IApplicationContextFactory contextFactory;
        private readonly ICurrentUser currentService;

        public Repository(IApplicationContextFactory contextFactory, ICurrentUser currentService ,IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.currentService = currentService;
            this.mapper = mapper;
        }

        public void Add(T obj)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Entry(obj).State = EntityState.Added;
                OnBeforeSaving(context);
                context.SaveChanges();
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> objCollection)
        {
            using (var context = contextFactory.GetContext())
            {
                await context.AddRangeAsync(objCollection);
                OnBeforeSaving(context);
                context.SaveChanges();
            }

            return objCollection.Count();
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
                return this.MultiInclude(context.Set<T>().AsNoTracking(), includes).Where(func).ToList();
            }
        }

        public void Update(T obj)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Attach(obj).State = EntityState.Modified;
                OnBeforeSaving(context);
                context.SaveChanges();
            }
        }

        public void SqlInject(string qurey)
        {
            using (var context = contextFactory.GetContext())
            {
                context.Database.ExecuteSqlCommand(qurey);
            }
        }

        private IQueryable<T> MultiInclude<TProperty>(IQueryable<T> query, params Expression<Func<T, TProperty>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }


        private void OnBeforeSaving(ApplicationContext context)
        {
            foreach (var entry in this.GetEntries<IHasCreatedBy>(context, EntityState.Added))
            {
                entry.Entity.CreateBy = currentService.CurrentUser;
            }

        }

        private IEnumerable<EntityEntry<TEntity>> GetEntries<TEntity>(ApplicationContext context, params EntityState[] entityStates) where TEntity : class 
            => context.ChangeTracker.Entries<TEntity>().Where(entry => entityStates.Contains(entry.State));
    }
}
