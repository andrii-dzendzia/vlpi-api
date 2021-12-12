using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class User : IEntity
    {
        public User()
        {
            TestResults = new HashSet<TestResult>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Studying { get; set; }
        public string Workplase { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
