using BusinessSolutionsLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Services
{
    public interface ITaskService
    {
        IReadOnlyList<Task> GetAll();

        IReadOnlyList<Task> Get(string title);

        Task Get(Guid id);

        Task Add(Guid id,Task task);

        void Update(Task task);

        bool Delete(Guid id);
    }
}
