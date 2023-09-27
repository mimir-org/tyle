using TypeLibrary.Core.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Common;

public interface IClassifierRepository : IReferenceRepository<RdlClassifier, RdlClassifierRequest>
{
}