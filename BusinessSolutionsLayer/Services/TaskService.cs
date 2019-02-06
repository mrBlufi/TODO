using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Repository;
using DataAccessLayer.Models;

namespace BusinessSolutionsLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskData> taskRepository;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public TaskService(IRepository<TaskData> taskRepository, IUsersService usersService , IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public Task Add(Guid id, Task task)
        {
            task.CreateBy = usersService.GetById(id);
            return mapper.Map<Task>(taskRepository.Add(mapper.Map<TaskData>(task)));
        }

        public bool Delete(Guid id)
        {
            var taskData = taskRepository.Get(x => x.Id == id).FirstOrDefault();

            if(taskData != null)
            {
                taskRepository.Delete(taskData);
                return true;
            }

            return false;
        }

        public IReadOnlyList<Task> Get(string title)
        {
            return mapper.Map<IReadOnlyList<Task>>(taskRepository.Get(x => x.Title.Contains(title)));
        }

        public Task Get(Guid id)
        {
            return mapper.Map<Task>(taskRepository.Get(x => x.Id == id));
        }

        public IReadOnlyList<Task> GetAll()
        {
            return mapper.Map<IReadOnlyList<Task>>(taskRepository.Get(x => true));
        }

        public void Update(Task task)
        {
            taskRepository.Update(mapper.Map<TaskData>(task));
        }
    }
}
