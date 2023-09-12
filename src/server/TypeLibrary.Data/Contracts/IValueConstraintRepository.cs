using System;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts;

public interface IValueConstraintRepository
{
    Task Update(ValueConstraint? valueConstraint, ValueConstraintRequest? request, Guid attributeId);
}
