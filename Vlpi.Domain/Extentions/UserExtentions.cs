using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Extentions
{
    static class UserExtentions
    {
        public static UserDto ToUserDto(this User user) =>
            new()
            {
                Email = user.Email,
                FirstName = user.Name,
                LastName = user.Surname,
                Id = user.Id,
                Role = user.Role,
                Studying = user.Studying,
                Workplace = user.Workplase,
            };

        public static AdminDto ToAdminDto(this User user) =>
            new()
            {
                Email = user.Email,
                FirstName = user.Name,
                LastName = user.Surname,
                Id = user.Id,
                Role = user.Role,
                Workplace = user.Workplase,
            };

        public static User ToUser(this RegisterDto registerDto) =>
            new()
            {
                Email = registerDto.Email,
                Name = registerDto.FirstName,
                Surname = registerDto.LastName,
                Password = registerDto.Password,
                Studying = registerDto.Studying,
                Workplase = registerDto.Workplace,
                Role = registerDto.Role,
                IsAdmin = registerDto.IsAdmin,
                UserAnswers = null,
                TestResults = null,
            };

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.Identity.Name);
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return bool.Parse(principal.Claims.First(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Value);
        }
    }
}
