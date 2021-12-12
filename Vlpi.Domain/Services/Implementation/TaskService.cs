using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vlpi.Data.Infrastructure;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;
using Vlpi.Domain.Extentions;
using Vlpi.Domain.Services.Interfaces;

namespace Vlpi.Domain.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Task> taskRepository;
        private readonly IRepository<Test> testRepository;
        private readonly IRepository<TestResult> testResultRepository;
        private readonly IRepository<AnswerBlock> answerBlockRepository;
        private readonly IRepository<UserAnswer> userAnswerRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TaskService(
            IRepository<Task> taskRepository,
            IRepository<Test> testRepository,
            IRepository<TestResult> testResultRepository,
            IRepository<AnswerBlock> answerBlockRepository, 
            IRepository<UserAnswer> userAnswerRepository, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.taskRepository = taskRepository;
            this.testRepository = testRepository;
            this.testResultRepository = testResultRepository;
            this.answerBlockRepository = answerBlockRepository;
            this.userAnswerRepository = userAnswerRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async System.Threading.Tasks.Task<Response> GetTask(int id)
        {
            var task = await taskRepository.Query().Include(t => t.AnswerBlocks).FirstAsync(t => t.Id == id);

            if (task is not null)
            {
                return new Response
                {
                    Status = "Ok",
                    Data = task.ToViewTaskDto(),
                };
            }
            else
            {
                return new Response
                {
                    Status = "Failed",
                    Message = "Task not found",
                    ErrorType = "NotFound",
                };
            }
        }

        public async System.Threading.Tasks.Task<Response> AddTask(AddTaskDto addTaskDto)
        {
            var task = await taskRepository.AddAsync(new Task
            {
                TestId = addTaskDto.TestId,
                Answer = "",
                Name = addTaskDto.Text,
                DifficultyLevel = 0,
            });
            await taskRepository.SaveChangesAsync();

            if(string.IsNullOrEmpty(addTaskDto.Text))
                task.Name = $"Task {task.Id + 1}";
            await taskRepository.SaveChangesAsync();

            return await UpdateTaskAnswerBlocks(task, addTaskDto.Blocks);

        }

        public async System.Threading.Tasks.Task<Response> EditTask(EditTaskDto editTaskDto)
        {
            var task = await taskRepository
                .Query()
                .Include(t => t.AnswerBlocks)
                .FirstAsync(t => t.Id == editTaskDto.TaskId);

            task.Name = editTaskDto.Text;
            await taskRepository.SaveChangesAsync();

            await answerBlockRepository.DeleteRangeAsync(task.AnswerBlocks);
            await answerBlockRepository.SaveChangesAsync();

            return await UpdateTaskAnswerBlocks(task, editTaskDto.Blocks);
        }

        public async System.Threading.Tasks.Task<Response> AnswerTask(TaskAnswerDto taskAnswerDto)
        {
            var task = await taskRepository
                .Query()
                .AsNoTrackingWithIdentityResolution()
                .Include(t => t.AnswerBlocks)
                .ThenInclude(block => block.InverseBetterAnswer)
                .FirstAsync(t => t.Id == taskAnswerDto.Id);

            var answerList = task.Answer.Split(' ').Select(int.Parse).ToList();

            var score = 0;

            for (int i = 0; i < Math.Min(answerList.Count, taskAnswerDto.Blocks.Count); i++)
            {
                if (answerList[i] == taskAnswerDto.Blocks[i])
                {
                    score += 3;
                }
                else if (task
                    .AnswerBlocks
                    .First(block => block.Id == answerList[i])
                    .InverseBetterAnswer
                    .Any(block => block.Id == taskAnswerDto.Blocks[i]))
                {
                    score += 1;
                }
            }

            var userAnswer = await userAnswerRepository.AddAsync(
                new()
                {
                    Answer = string.Join(' ', taskAnswerDto.Blocks),
                    TaskId = taskAnswerDto.Id,
                    UserId = httpContextAccessor.HttpContext.User.GetUserId(),
                    TestResultId = (await testResultRepository
                        .Query()
                        .AsNoTrackingWithIdentityResolution()
                        .FirstAsync(tr => 
                            tr.IsInProgress &&
                            tr.UserId == httpContextAccessor.HttpContext.User.GetUserId() && 
                            tr.TestId == task.TestId)).Id,
                    Score = Convert.ToInt32(100.0 * score / (answerList.Count * 3.0)),
                });
            await userAnswerRepository.SaveChangesAsync();

            var test = await testRepository
                .Query()
                .AsNoTrackingWithIdentityResolution()
                .Include(_test => _test.Tasks)
                    .ThenInclude(_task => _task.AnswerBlocks)
                .Include(_test => _test.TestResults)
                    .ThenInclude(_testResult => _testResult.UserAnswers)
                .FirstAsync(_test => _test.Id == task.TestId);

            var testResult = test.TestResults.First(_testResult => _testResult.Id == userAnswer.TestResultId && _testResult.IsInProgress);

            score = Convert.ToInt32(1.0 * testResult.UserAnswers.Sum(_userAnswer => _userAnswer.Score) / test.Tasks.Count);
            var testsPassed = testResult.UserAnswers.Count;

            testResult = await testResultRepository.GetByIdAsync(testResult.Id);
            testResult.Score = score;

            if (test.Tasks.Count == testsPassed)
                testResult.IsInProgress = false;

            await testResultRepository.SaveChangesAsync();

            if (test.Tasks.Count == testsPassed)
            {
                return new Response
                {
                    Status = "Ok",
                };
            }
            else
            {
                return new Response
                {
                    Status = "Ok",
                    Data = test.Tasks.ElementAt(testsPassed).ToAnswerTaskDto(testsPassed),
                };
            }
        }

        private async System.Threading.Tasks.Task<Response> UpdateTaskAnswerBlocks(Task task, List<EditBlockDto> blocks)
        {
            var answerBlockDtos = blocks.Where(block => block.IsEnabled);
            var answerBlocks = new List<AnswerBlock>();

            foreach (var block in answerBlockDtos)
            {
                var answerBlock = await answerBlockRepository.AddAsync(new AnswerBlock
                {
                    TaskId = task.Id,
                    IsCorrect = block.IsEnabled,
                    Text = block.Text,
                });
                await answerBlockRepository.SaveChangesAsync();
                answerBlocks.Add(answerBlock);
            }

            task.Answer = string.Join(" ", answerBlocks.Select(ab => ab.Id.ToString()));
            task.DifficultyLevel = GetDifficulty(blocks);
            await taskRepository.SaveChangesAsync();

            var redurantBlockDtos = blocks.Where(block => !block.IsEnabled);

            foreach (var block in redurantBlockDtos)
            {
                var answerBlock = await answerBlockRepository.AddAsync(new AnswerBlock
                {
                    TaskId = task.Id,
                    IsCorrect = block.IsEnabled,
                    Text = block.Text,
                    BetterAnswerId = block.LinkedBlockPosition is null ?
                        null :
                        answerBlocks.First(answerBlock => answerBlock.Text == blocks[block.LinkedBlockPosition.Value].Text).Id,
                });
                await answerBlockRepository.SaveChangesAsync();
            }

            task = taskRepository.Query().Include(t => t.AnswerBlocks).First(t => t.Id == task.Id);

            return new Response
            {
                Status = "Ok",
                Data = task.ToViewTaskDto(),
            };
        }

        private static int GetDifficulty(List<EditBlockDto> blocks)
        {
            var redurantBlocks = blocks.Where(b => !b.IsEnabled);
            if (!redurantBlocks.Any())
            {
                return 1;
            }
            if (redurantBlocks.All(rb => rb.LinkedBlockPosition is null))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
