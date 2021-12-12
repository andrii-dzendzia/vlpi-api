using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vlpi.Data.Infrastructure;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;
using Vlpi.Domain.Extentions;
using Vlpi.Domain.Services.Interfaces;

namespace Vlpi.Domain.Services.Implementation
{
    public class ModuleService : IModuleService
    {
        private readonly IRepository<Module> moduleRepository;
        private readonly IRepository<Emoji> emojiRepository;

        public ModuleService(IRepository<Module> moduleRepository, IRepository<Emoji> emojiRepository)
        {
            this.moduleRepository = moduleRepository;
            this.emojiRepository = emojiRepository;
        }

        public async Task<Response> GetModulesAsync()
        {
            var emoji = await emojiRepository.Query().ToListAsync();

            return new()
            {
                Status = "Ok",
                Data = moduleRepository
                    .Query()
                    .AsEnumerable()
                    .Select(module => 
                        module.ToDto(emoji.First(e => e.Name == module.Name + (module.IsEnabled ? " enabled" : " disabled")).Id)).ToList(),
            };
        }
    }
}
