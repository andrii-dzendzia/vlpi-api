using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vlpi.Data.Models;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Extentions
{
    public static class ModuleExtentions
    {
        public static ModuleDto ToDto(this Module module, int emojiId) =>
            new()
            {
                Name = module.Name,
                Id = module.Id,
                EmojiId = emojiId,
                IsEnabled = module.IsEnabled,
            };
    }
}
