using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Extentions
{
    public static class TaskExtentions
    {
        public static PreviewTaskDto ToPreviewTaskDto(this Task task) =>
            new()
            {
                Id = task.Id,
                Text = task.Name,
                DifficultyLevel = task.DifficultyLevel,
            };

        public static ViewTaskDto ToViewTaskDto(this Task task) =>
            new()
            {
                Id = task.Id,
                Text = task.Name,
                DifficultyLevel = task.DifficultyLevel,
                Blocks = task.AnswerBlocks.Select(ab => ab.ToViewBlockDto()).ToList()
            };

        public static AnswerTaskDto ToAnswerTaskDto(this Task task, int orderNumber) =>
            new()
            {
                Id = task.Id,
                CurrentOrderNumber = orderNumber + 1,
                Text = task.Name,
                Blocks = task.AnswerBlocks.Select(block => block.ToAnswerBlockDto()).ToList(),
            };
    }
}
