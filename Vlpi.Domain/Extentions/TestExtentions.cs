using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Extentions
{
    public static class TestExtentions
    {
        public static AdminTestDto ToAdminTestDto(this Test test) =>
            new()
            {
                Name = test.Name,
                Id = test.Id,
                Status = test.Status,
                TaskCount = test.Tasks.Count,
                Tasks = test.Tasks.Select(task => task.ToPreviewTaskDto()).ToList(),
            };

        public static UserTestDto ToUserTestDto(this Test test, int userId)
        {
            TestResult currentResult = null;
            int? bestScore = null;

            if (test.TestResults.Where(t => t.UserId == userId).Any())
            {
                bestScore = test.TestResults.Where(t => t.UserId == userId).Max(t => t.Score);
                currentResult = test.TestResults.FirstOrDefault(t => t.IsInProgress && t.UserId == userId) ??
                    test.TestResults.FirstOrDefault(t => t.Score == bestScore && t.UserId == userId);
            }

            return new UserTestDto
            {
                Id = test.Id,
                Name = test.Name,
                TaskCount = test.Tasks.Count,
                BestScore = bestScore,
                IsInProgres = currentResult?.IsInProgress ?? false,
                TaskPassed = currentResult?.UserAnswers.Count ?? 0,
            };
        }
    }
}
