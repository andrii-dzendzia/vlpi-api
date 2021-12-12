using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Services.Interfaces
{
    public interface IModuleService
    {
        Task<Response> GetModulesAsync();
    }
}
