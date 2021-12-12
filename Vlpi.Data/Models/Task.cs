using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class Task : IEntity
    {
        public Task()
        {
            AnswerBlocks = new HashSet<AnswerBlock>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Answer { get; set; }
        public int TestId { get; set; }
        public int DifficultyLevel { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<AnswerBlock> AnswerBlocks { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
