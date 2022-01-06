using System.Collections.Generic;
using System.Threading.Tasks;
using Mb.Models.Application;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.Enums;
using Mb.Models.Enums;

namespace TypeLibrary.Services.Contracts
{
    public interface IEnumService
    {
        Task<EnumBase> CreateEnum(CreateEnum createEnum);
        IEnumerable<EnumBase> GetAllOfType(EnumType enumType);
        IEnumerable<LocationTypeAm> GetAllLocationTypes();
    }
}
