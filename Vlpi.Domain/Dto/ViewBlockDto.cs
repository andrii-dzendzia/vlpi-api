using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class ViewBlockDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsEnabled { get; set; }
        public int? LinkedBlockId { get; set; }

    }
}
