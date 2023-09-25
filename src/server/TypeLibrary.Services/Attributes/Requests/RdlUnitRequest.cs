using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Attributes.Requests;

public class RdlUnitRequest : RdlObjectRequest
{
    public string? Symbol { get; set; }
}