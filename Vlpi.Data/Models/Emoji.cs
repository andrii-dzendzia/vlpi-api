using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Data.Models
{
    public class Emoji : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
