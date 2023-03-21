using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IUnitService
{
    IEnumerable<UnitLibCm> Get();
    UnitLibCm Get(int id);
}