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
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login) => Ok(await userService.Login(login));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDto register) => Ok(await userService.Register(register));

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto updateDto) => Ok(await userService.Update(updateDto));

        [HttpGet("statistic")]
        public async Task<IActionResult> GetStatistic() => Ok(await userService.GetStatistic());
    }
}
