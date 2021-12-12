using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class Test : IEntity
    {
        public Test()
        {
            Tasks = new HashSet<Task>();
            TestResults = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int ModuleId { get; set; }
        public int AdminId { get; set; }

        public virtual User Admin { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
