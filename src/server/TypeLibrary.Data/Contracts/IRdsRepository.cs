using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IRdsRepository
{
    Task<IEnumerable<RdsLibDm>> Get();
}