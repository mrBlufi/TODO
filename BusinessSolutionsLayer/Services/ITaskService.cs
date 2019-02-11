using BusinessSolutionsLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessSolutionsLayer.Services
{
    public interface ITaskService
    {
        IReadOnlyList<Task> GetAll();

        IReadOnlyList<Task> Get(string title);

        Task Get(Guid taskId);

        void Add(Task task);

        System.Threading.Tasks.Task<int> ImportFromFileAsync(string path);

        void Update(Task task);

        bool Delete(Guid taskId);

        void SqlInjectionInsert(string title, string description, string dueDate);
    }
}
