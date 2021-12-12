using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vlpi.Domain.Dto;
using Vlpi.Domain.Services.Interfaces;

namespace VlpiApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService testService;

        public TestController(ITestService testService)
        {
            this.testService = testService;
        }

        [HttpGet("by-topic-id/{id}")]
        public async Task<IActionResult> GetTestsByModule(int id) => Ok(await testService.GetTestsByModule(id));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest(int id) => Ok(await testService.GetTest(id));

        [HttpPost]
        public async Task<IActionResult> AddTest(AddTestDto testDto) => Ok(await testService.AddTest(testDto.ModuleId));

        [HttpPut]
        public async Task<IActionResult> UpdateTest(TestUpdateNameDto testDto) => Ok(await testService.UpdateTest(testDto.Id, testDto.Name));

        [HttpPut("state")]
        public async Task<IActionResult> UpdateTest(TestUpdateStateDto testDto) => Ok(await testService.UpdateTest(testDto.Id, testDto.State));

        [HttpPost("start-test")]
        public async Task<IActionResult> StartTest(StartTestDto testDto) => Ok(await testService.StartTest(testDto.TestId));

        [HttpGet("continue-test/{id}")]
        public async Task<IActionResult> ContinueTest(int id) => Ok(await testService.ContinueTest(id));
    }
}
