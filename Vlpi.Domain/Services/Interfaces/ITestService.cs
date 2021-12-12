using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Services.Interfaces
{
    public interface ITestService
    {
        Task<Response> GetTestsByModule(int moduleId);
        Task<Response> GetTest(int id);
        Task<Response> AddTest(int moduleId);
        Task<Response> UpdateTest(int id, string name);
        Task<Response> UpdateTest(int id, int state);
        Task<Response> StartTest(int testId);
        Task<Response> ContinueTest(int id);
    }
}
