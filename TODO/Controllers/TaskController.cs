using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Services;
using BusinessSolutionsLayer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IFileService fileService;

        public TaskController(ITaskService taskService, IFileService fileService)
        {
            this.taskService = taskService;
            this.fileService = fileService;
        }

        [HttpGet]
        [ResponseCache(Duration = 180)]
        public IActionResult Get()
        {
            return Ok(taskService.GetAll());
        }

        [HttpGet("{title}")]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(string title)
        {
            return Ok(taskService.Get(title));
        }

        [AuthFilter(RoleId.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            taskService.Add(task);
            return Ok();
        }

        [AuthFilter(RoleId.Admin)]
        [HttpPost]
        [Route("api/task/import")]
        [RequestSizeLimit(int.MaxValue)]
        public async System.Threading.Tasks.Task<IActionResult> Import(IFormFile file)
        {
            string path;
            using (var stream = file.OpenReadStream())
            {
                path = await fileService.SaveFileAsync(stream);
            }

            try
            {
                var count = await taskService.ImportFromFileAsync(path);
                return Ok();
            }
            finally
            {
                fileService.DeleteFile(path);
            }
        }

        [HttpGet("{title}/{description}/{duedate}")]
        public IActionResult SqlInsert(string title, string description, string duedate)
        {
            taskService.SqlInjectionInsert(title, description, duedate);
            return Ok();
        }

        [AuthFilter(RoleId.Admin)]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Task task)
        {
            task.Id = id;
            taskService.Update(task);
            return Ok();
        }

        [AuthFilter(RoleId.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (taskService.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}