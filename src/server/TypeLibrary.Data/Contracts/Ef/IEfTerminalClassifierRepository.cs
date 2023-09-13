using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts.Ef;

public interface IEfTerminalClassifierRepository : IGenericRepository<TypeLibraryDbContext, TerminalClassifierMapping>
{

}