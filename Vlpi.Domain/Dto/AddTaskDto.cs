using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class AddTaskDto
    {
        public int TestId { get; set; }
        public string Text { get; set; }
        public List<EditBlockDto> Blocks { get; set; }
    }
}
