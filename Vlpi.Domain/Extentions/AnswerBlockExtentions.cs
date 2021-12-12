using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Extentions
{
    public static class AnswerBlockExtentions
    {
        public static ViewBlockDto ToViewBlockDto(this AnswerBlock answerBlock) =>
            new()
            {
                Id = answerBlock.Id,
                IsEnabled = answerBlock.IsCorrect,
                LinkedBlockId = answerBlock.BetterAnswerId,
                Text = answerBlock.Text,
            };

        public static AnswerBlockDto ToAnswerBlockDto(this AnswerBlock answerBlock) =>
            new()
            {
                Id = answerBlock.Id,
                Text = answerBlock.Text,
            };
    }
}
