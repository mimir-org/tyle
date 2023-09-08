using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef;

public interface IEfValueConstraintRepository : IGenericRepository<TypeLibraryDbContext, ValueConstraint>, IValueConstraintRepository
{

}