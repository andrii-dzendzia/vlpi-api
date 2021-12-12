using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class ViewTaskDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<ViewBlockDto> Blocks { get; set; }
        public int DifficultyLevel { get; set; }
    }
}
