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
            var userId = Guid.Parse(ControllerContext.HttpContext.Request.Cookies[CookiesKeys.ID]);
            taskService.Add(userId, task);
            return Ok();
        }

        [AuthFilter(RoleId.Admin)]
        [HttpPost]
        [Route("api/task/import")]
        [RequestSizeLimit(int.MaxValue)]
        public async System.Threading.Tasks.Task<IActionResult> Import(IFormFile file)
        {
            var userId = Guid.Parse(ControllerContext.HttpContext.Request.Cookies[CookiesKeys.ID]);
            string path;
            using (var stream = file.OpenReadStream())
            {
                path = await fileService.SaveFileAsync(stream);
            }

            try
            {
                var count = await taskService.ImprotFromFileAsync(userId, path);
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
            taskService.SqlInjectionInsert(Guid.Parse(ControllerContext.HttpContext.Request.Cookies[CookiesKeys.ID]),
                title, description, duedate);
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