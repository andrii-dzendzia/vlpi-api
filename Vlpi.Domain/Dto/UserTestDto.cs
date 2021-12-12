using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class UserTestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskCount { get; set; }
        public int TaskPassed { get; set; }
        public int? BestScore { get; set; }
        public bool IsInProgres { get; set; }

    }
}
