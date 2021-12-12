using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class TaskAnswerDto
    {
        public int Id { get; set; }
        public List<int> Blocks { get; set; }
    }
}
