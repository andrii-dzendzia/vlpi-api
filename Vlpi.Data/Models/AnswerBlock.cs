using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class AnswerBlock : IEntity
    {
        public AnswerBlock()
        {
            InverseBetterAnswer = new HashSet<AnswerBlock>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int? BetterAnswerId { get; set; }
        public int TaskId { get; set; }

        public virtual AnswerBlock BetterAnswer { get; set; }
        public virtual Task Task { get; set; }
        public virtual ICollection<AnswerBlock> InverseBetterAnswer { get; set; }
    }
}
