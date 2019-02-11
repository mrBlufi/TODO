using AutoMapper;
using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Repository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSolutionsLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskData> taskRepository;
        private readonly IUserService usersService;
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public TaskService(IRepository<TaskData> taskRepository, IUserService usersService, IFileService fileService, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.usersService = usersService;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        public void SqlInjectionInsert(Guid creatBy, string title, string description, string dueDate)
        {
            var builder = new StringBuilder()
                .AppendFormat("INSERT INTO [dbo].Tasks([Id],[CreateById],[Title],[Description],[DueDate]) VALUES('{0}','{1}','{2}','{3}','{4}')",
                Guid.NewGuid().ToString(), creatBy.ToString(), title, description, dueDate);

            taskRepository.SqlInject(builder.ToString());
        }

        public void Add(Guid userId, Task task)
        {
            task.CreateBy = usersService.Get(userId);
            taskRepository.Add(mapper.Map<TaskData>(task));
        }

        public bool Delete(Guid taskId)
        {
            var taskData = taskRepository.Get(x => x.Id == taskId).FirstOrDefault();

            if (taskData != null)
            {
                taskRepository.Delete(taskData);
                return true;
            }

            return false;
        }

        public IReadOnlyList<Task> Get(string title)
        {
            return mapper.Map<IReadOnlyList<Task>>(taskRepository.Get(x => x.Title.Contains(title), x => x.CreateBy));
        }

        public Task Get(Guid taskId)
        {
            return mapper.Map<Task>(taskRepository.Get(x => x.Id == taskId, i => i.CreateBy));
        }

        public IReadOnlyList<Task> GetAll()
        {
            return mapper.Map<IReadOnlyList<Task>>(taskRepository.Get(x => true, i => i.CreateBy));
        }

        public async System.Threading.Tasks.Task<int> ImportFromFileAsync(Guid userId, string path)
        {
            var createBy = mapper.Map<UserData>(usersService.Get(userId));
            var tasks = await fileService.ParseFileAsync<TaskData>(path);
            foreach (var item in SplitCollection(tasks))
            {
                await taskRepository.AddRangeAsync(item.Select(x =>
                {
                    x.CreateBy = createBy;
                    return x;
                }), createBy);
            }
            return tasks.Count();
        }

        public void Update(Task task)
        {
            taskRepository.Update(mapper.Map<TaskData>(task));
        }

        private IEnumerable<IEnumerable<T>> SplitCollection<T>(IEnumerable<T> locations, int nSize = 100)
        {
            for (int i = 0; i < locations.Count(); i += nSize)
            {
                yield return locations.Skip(i).Take(Math.Min(nSize, locations.Count() - i));
            }
        }
    }
}
