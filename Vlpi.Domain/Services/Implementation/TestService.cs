using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vlpi.Data.Infrastructure;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;
using Vlpi.Domain.Enums;
using Vlpi.Domain.Extentions;
using Vlpi.Domain.Services.Interfaces;

namespace Vlpi.Domain.Services.Implementation
{
    public class TestService : ITestService
    {
        private readonly IRepository<Test> testRepository;
        private readonly IRepository<TestResult> testResultRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TestService(IRepository<Test> testRepository, IRepository<TestResult> testResultRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.testRepository = testRepository;
            this.testResultRepository = testResultRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response> GetTestsByModule(int ModuleId)
        {
            if (httpContextAccessor.HttpContext.User.IsAdmin())
            {
                return new Response
                {
                    Status = "Ok",
                    Data = 
                        await testRepository.Query()
                            .Include(test => test.Tasks)
                            .Where(test => test.ModuleId == ModuleId)
                            .Select(test => new AdminTestListDto
                            {
                                Id = test.Id,
                                Name = test.Name,
                                Status = test.Status,
                                TaskCount = test.Tasks.Count,
                            }).ToListAsync(),
                };
            }
            else
            {
                var userId = httpContextAccessor.HttpContext.User.GetUserId();

                return new Response
                {
                    Status = "Ok",
                    Data = testRepository.Query()
                            .Include(test => test.Tasks)
                            .Include(test => test.TestResults)
                            .Where(test => test.ModuleId == ModuleId && test.Status == (int)TestStatus.Published)
                            .AsEnumerable()
                            .Select(test => test.ToUserTestDto(userId))
                            .ToList(),
                };
            }
        }

        public async Task<Response> GetTest(int id)
        {
            if (httpContextAccessor.HttpContext.User.IsAdmin())
            {

                return new Response
                {
                    Status = "Ok",
                    Data = new
                    {
                        Test = (await testRepository.Query()
                            .Include(test => test.Tasks)
                            .FirstAsync(test => test.Id == id))
                            .ToAdminTestDto(),
                        Statistics = await GetAdminStatistics(id),
                    },
                };
            }
            else
            {
                var userId = httpContextAccessor.HttpContext.User.GetUserId();
                var test = await testRepository
                    .Query()
                    .Include(t => t.TestResults)
                    .FirstAsync(t => t.Id == id);
                var triesCount = test
                    .TestResults
                    .Count();
                var seccessfulCount = test
                    .TestResults
                    .Count(t => t.Score >= 80);
                var userTestResults = test.TestResults.Where(t => t.UserId == userId).ToList();

                return new Response
                {
                    Status = "Ok",
                    Data = new
                    {
                        Test = (await testRepository.Query()
                            .Include(test => test.Tasks)
                            .Include(test => test.TestResults)
                            .FirstAsync(test => test.Id == id))
                            .ToUserTestDto(userId),
                        Statistics = new Dictionary<string, string>
                        {
                            {
                                "Total tries",
                                triesCount.ToString()
                            },
                            {
                                "Total averange rate",
                                (triesCount > 0 ? Convert.ToInt32(100.0 * seccessfulCount / triesCount) : 0).ToString() + "%"
                            },
                            {
                                "Your tries",
                                userTestResults.Count().ToString()
                            },
                            {
                                "Seccessful tries",
                                userTestResults.Count(t => t.Score >= 80).ToString()
                            },
                            {
                                "Averange score",
                                (userTestResults.Count > 0 ? Convert.ToInt32(1.0 * userTestResults.Sum(t => t.Score) / userTestResults.Count) : 0).ToString() + "%"
                            },
                        },
                    },
                };
            }
        }

        public async Task<Response> AddTest(int moduleId)
        {
            var test = await testRepository.AddAsync(new Test 
            { 
                ModuleId = moduleId,
                Name = "",
                Status = (int)TestStatus.Draft,
                AdminId = httpContextAccessor.HttpContext.User.GetUserId(),
            });
            await testRepository.SaveChangesAsync();

            test.Name = $"Test {test.Id + 1}";
            await testRepository.SaveChangesAsync();

            return new Response
            {
                Status = "Ok",
                Data = test.ToAdminTestDto(),
            };
        }

        public async Task<Response> UpdateTest(int id, string name)
        {
            var test = await testRepository.GetByIdAsync(id);
            test.Name = name;
            await testRepository.SaveChangesAsync();

            return new Response
            {
                Status = "Ok",
                Data = test.ToAdminTestDto(),
            };
        }

        public async Task<Response> UpdateTest(int id, int state)
        {
            var test = await testRepository.Query().Include(t => t.Tasks).FirstAsync(t => t.Id == id);
            test.Status = state;
            await testRepository.SaveChangesAsync();

            return new Response
            {
                Status = "Ok",
                Data = new
                {
                    Test = test.ToAdminTestDto(),
                    Statistics = await GetAdminStatistics(id),
                },
            };
        }

        public async Task<Response> StartTest(int testId)
        {
            var test = await testRepository
                .Query()
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.AnswerBlocks)
                .FirstAsync(t => t.Id == testId);
            
            var testResult = await testResultRepository.AddAsync(new TestResult
            {
                IsInProgress = true,
                Score = 0,
                TestId = testId,
                UserId = httpContextAccessor.HttpContext.User.GetUserId(),
            });
            await testResultRepository.SaveChangesAsync();

            return new Response
            {
                Status = "Ok",
                Data = test.Tasks.First().ToAnswerTaskDto(0),
            };
        }

        public async Task<Response> ContinueTest(int id)
        {
            var test = await testRepository
                .Query()
                .Include(t => t.Tasks)
                    .ThenInclude(t => t.AnswerBlocks)
                .FirstAsync(t => t.Id == id);

            var testResult = await testResultRepository
                .Query()
                .Include(testResult => testResult.UserAnswers)
                .FirstAsync(testResult =>
                    testResult.IsInProgress &&
                    testResult.UserId == httpContextAccessor.HttpContext.User.GetUserId());

            return new Response
            {
                Status = "Ok",
                Data = test.Tasks
                        .ElementAt(testResult.UserAnswers.Count)
                        .ToAnswerTaskDto(testResult.UserAnswers.Count),
            };
        }

        private async Task<Dictionary<string, string>> GetAdminStatistics(int id)
        {
            var triesCount = (await testRepository
                                .Query()
                                .Include(t => t.TestResults)
                                .FirstAsync(t => t.Id == id))
                                    .TestResults
                                    .Count();
            var seccessfulCount = (await testRepository
                                .Query()
                                .Include(t => t.TestResults)
                                .FirstAsync(t => t.Id == id))
                                    .TestResults
                                    .Count(t => t.Score >= 80);

            return new Dictionary<string, string>
            {
                {
                    "Students involved",
                    (await testRepository
                        .Query()
                        .Include(t => t.TestResults)
                        .FirstAsync(t=>t.Id == id))
                            .TestResults
                            .Select(t => t.UserId)
                            .Distinct()
                            .Count()
                            .ToString()
                },
                {
                    "Amount of tries",
                    triesCount.ToString()
                },
                {
                    "Seccessful tries",
                    seccessfulCount.ToString()
                },
                {
                    "Seccessful tries rate",
                    (triesCount > 0 ? Convert.ToInt32(100.0 * seccessfulCount / triesCount) : 0).ToString()+"%"
                }
            };
        }
    }
}
