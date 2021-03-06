using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class AdminTestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskCount { get; set; }
        public int Status { get; set; }
        public List<PreviewTaskDto> Tasks { get; set; }
    }
}
