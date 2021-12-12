using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vlpi.Domain.Services.Interfaces;
using Vlpi.Domain.Dto;
using Microsoft.AspNetCore.Authorization;

namespace VlpiApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id) => Ok(await taskService.GetTask(id));

        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskDto addTaskDto) => Ok(await taskService.AddTask(addTaskDto));

        [HttpPut]
        public async Task<IActionResult> EditTask(EditTaskDto editTaskDto) => Ok(await taskService.EditTask(editTaskDto));

        [HttpPost("answer")]
        public async Task<IActionResult> AnswerTask(TaskAnswerDto addTaskDto) => Ok(await taskService.AnswerTask(addTaskDto));
    }
}
