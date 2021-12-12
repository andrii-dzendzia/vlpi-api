using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class UserAnswer : IEntity
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int TestResultId { get; set; }

        public virtual Task Task { get; set; }
        public virtual TestResult TestResult { get; set; }
        public virtual User User { get; set; }
    }
}
