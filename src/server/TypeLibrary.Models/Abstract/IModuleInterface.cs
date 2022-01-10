using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Abstract
{
    public interface IModuleInterface
    {
        void CreateModule(IServiceCollection services, IConfiguration configuration);
        ICollection<Profile> GetProfiles();
        ModuleDescription GetModuleDescription();
    }
}
