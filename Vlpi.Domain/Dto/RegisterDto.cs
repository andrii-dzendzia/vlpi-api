using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Studying { get; set; }
        public string? Workplace { get; set; }
        public string? Role { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        public bool IsAdmin { get; set; }
    }
}

