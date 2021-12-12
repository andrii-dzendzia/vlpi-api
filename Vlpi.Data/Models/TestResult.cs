using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class TestResult : IEntity
    {
        public TestResult()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public int Score { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public bool IsInProgress { get; set; }

        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
