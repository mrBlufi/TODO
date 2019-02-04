using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessSolutionsLayer.Models;
using DataAccessLayer.Models;

namespace BusinessSolutionsLayer
{
    public interface ITaskRepository
    {
        void Add(Task task);
        IEnumerable<Task> Get(Expression<Func<TaskData, bool>> func);
        void Update(Task task);
    }
}