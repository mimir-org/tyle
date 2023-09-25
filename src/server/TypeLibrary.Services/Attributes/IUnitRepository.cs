using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes.Requests;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Services.Attributes;

public interface IUnitRepository : IReferenceRepository<RdlUnit, UnitReferenceRequest>
{
}