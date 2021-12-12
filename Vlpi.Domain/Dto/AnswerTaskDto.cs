using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class AnswerTaskDto
    {
        public int Id { get; set; }
        public int CurrentOrderNumber { get; set; }
        public string Text { get; set; }
        public List<AnswerBlockDto> Blocks { get; set; }
    }
}
