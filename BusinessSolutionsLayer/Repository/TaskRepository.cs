using AutoMapper;
using BusinessSolutionsLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BusinessSolutionsLayer
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMapper mapper;

        public TaskRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<Task> Get(Expression<Func<TaskData, bool>> func)
        {
            using (var context = new ApplicationContext())
            {
                return mapper.Map<IEnumerable<Task>>(context.Tasks.AsNoTracking().Where(func));
            }
        }

        public void Add(Task task)
        {
            using (var context = new ApplicationContext())
            {
                context.Entry(mapper.Map<TaskData>(task)).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Task task)
        {
            using (var context = new ApplicationContext())
            {
                context.Attach(mapper.Map<TaskData>(task)).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
