using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Terminals.Requests;

namespace TypeLibrary.Services.Terminals;

public interface IMediumRepository : IReferenceRepository<RdlMedium, MediumReferenceRequest>
{
}