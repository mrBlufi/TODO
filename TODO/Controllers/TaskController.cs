using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Services;
using BusinessSolutionsLayer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(taskService.GetAll());
        }


        [HttpGet("{title}")]
        public IActionResult Get(string title)
        {
            return Ok(taskService.Get(title));
        }

        [AuthFilter(RoleId.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            var userId = Guid.Parse(ControllerContext.HttpContext.Request.Cookies[CookiesKeys.ID]);
            task = taskService.Add(userId, task);
            return Ok(task);
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
                return Ok("successfully deleted");
            }
            return BadRequest("Somthing wrong");
        }

    }
}