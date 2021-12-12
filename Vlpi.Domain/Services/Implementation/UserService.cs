using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vlpi.Data.Infrastructure;
using Vlpi.Data.Models;
using Vlpi.Domain.Configurations;
using Vlpi.Domain.Dto;
using Vlpi.Domain.Extentions;
using Vlpi.Domain.Services.Interfaces;

namespace Vlpi.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Test> testRepository;
        private readonly IRepository<TestResult> testResultRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(
            IRepository<User> userRepository,
            IRepository<Test> testRepository,
            IRepository<TestResult> testResultRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.testRepository = testRepository;
            this.testResultRepository = testResultRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response> Login(LoginDto login)
        {
            var user = await userRepository
                .Query()
                .FirstOrDefaultAsync(user
                    => user.Email == login.Email &&
                        user.Password == login.Password);

            if (user is not null)
            {
                var identity = GetIdentity(user);

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: JwtTokenOptions.ISSUER,
                        audience: JwtTokenOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(JwtTokenOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(JwtTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                if (login.IsAdmin)
                {
                    if (user.IsAdmin)
                    {
                        return new Response
                        {
                            Status = "Ok",
                            Data = new { User = user.ToAdminDto(), Token = encodedJwt},
                        };
                    }
                    else
                    {
                        return new Response
                        {
                            Status = "Failed",
                            Data = user.ToUserDto(),
                            Message = "User isn't administrator",
                            ErrorType = "BadRequest",
                        };
                    }
                }
                else
                {
                    return new Response
                    {
                        Status = "Ok",
                        Data = new { User = user.ToUserDto(), Token = encodedJwt },
                    };
                }
            }
            else
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "User not found",
                    ErrorType = "NotFound",
                };
            }
        }

        public async Task<Response> Register(RegisterDto register)
        {
            if(register.Password == register.PasswordRepeat)
            {
                var user = register.ToUser();
                await userRepository.AddAsync(user);
                await userRepository.SaveChangesAsync();

                return await Login(new LoginDto { Email = register.Email, Password = register.Password, IsAdmin = register.IsAdmin });
            }
            else
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "Passwords must be equals",
                    ErrorType = "PasswordsDon'tMatch",
                };
            }
        }

        public async Task<Response> Update(UserUpdateDto updateDto)
        {
            var user = await userRepository.GetByIdAsync(httpContextAccessor.HttpContext.User.GetUserId());

            if (user is not null)
            {
                user.Name = updateDto.FirstName;
                user.Surname = updateDto.LastName;
                user.Workplase = updateDto.Workplace;
                user.Role = updateDto.Role;
                user.Studying = updateDto.Studying;
                await userRepository.SaveChangesAsync();


                if (user.IsAdmin)
                {
                    return new Response
                    {
                        Status = "Ok",
                        Data = user.ToAdminDto(),
                    };
                }
                else
                {
                    return new Response
                    {
                        Status = "Ok",
                        Data = user.ToUserDto(),
                    };
                }
            }
            else
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "User not found",
                    ErrorType = "NotFound",
                };
            }
        }

        public async Task<Response> GetStatistic()
        {
            var statictics = new List<UserStatisticDto>();

            var userId = httpContextAccessor.HttpContext.User.GetUserId();

            if (httpContextAccessor.HttpContext.User.IsAdmin())
            {
                statictics.Add(
                    new()
                    {
                        Label = "Test Published",
                        EmojiId = 11,
                        Value = (await testRepository.Query().CountAsync()).ToString(),
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Students",
                        EmojiId = 12,
                        Value = (await userRepository.Query().CountAsync(u => !u.IsAdmin)).ToString(),
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Attempts",
                        EmojiId = 10,
                        Value = (await testResultRepository.Query().CountAsync()).ToString(),
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Success Rate",
                        EmojiId = 9,
                        Value = (statictics.Last().Value != "0" ? Convert.ToInt32(await testResultRepository.Query().SumAsync(t => t.Score) / Convert.ToDouble(statictics.Last().Value)) : 0).ToString() + "%",
                    });
            }
            else
            {
                var user = await userRepository
                    .Query()
                    .Include(u => u.TestResults)
                    .Include(u => u.UserAnswers)
                    .FirstAsync(u => u.Id == userId);

                statictics.Add(
                    new()
                    {
                        Label = "Test Taken",
                        EmojiId = 11,
                        Value = user.TestResults.Count.ToString(),
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Passed Test",
                        EmojiId = 12,
                        Value = (user.TestResults.Count > 0 ? Convert.ToInt32(100.0 * user.TestResults.Count(t=>t.Score>=80) / user.TestResults.Count) : 0).ToString() + "%",
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Task Taken",
                        EmojiId = 10,
                        Value = user.UserAnswers.Count.ToString(),
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Passed Task",
                        EmojiId = 9,
                        Value = (user.UserAnswers.Count > 0 ? Convert.ToInt32(100.0 * user.UserAnswers.Count(t => t.Score >= 80) / user.UserAnswers.Count) : 0).ToString() + "%",
                    });
                statictics.Add(
                    new()
                    {
                        Label = "Averange Rate",
                        EmojiId = 13,
                        Value = (user.TestResults.Count > 0 ? Convert.ToInt32(1.0 * user.TestResults.Sum(t => t.Score) / user.TestResults.Count) : 0).ToString() + "%",
                    });
            }

            return new Response
            {
                Status = "Ok",
                Data = statictics,
            };
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IsAdmin.ToString())
                };
            ClaimsIdentity claimsIdentity =
                new(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
