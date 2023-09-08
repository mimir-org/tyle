using System;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IValueConstraintRepository
{
    Task Update(ValueConstraint? valueConstraint, ValueConstraintLibAm? valueConstraintAm, Guid attributeId);
}
