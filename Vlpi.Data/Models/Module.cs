using System;
using System.Collections.Generic;

#nullable disable

namespace Vlpi.Data.Models
{
    public partial class Module : IEntity
    {
        public Module()
        {
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
