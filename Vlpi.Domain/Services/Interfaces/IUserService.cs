using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response> Login(LoginDto login);

        Task<Response> Register(RegisterDto register);

        Task<Response> Update(UserUpdateDto update);

        Task<Response> GetStatistic();
    }
}
