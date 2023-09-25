using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Attributes.Requests;

public class UnitReferenceRequest : ReferenceRequest
{
    public string? Symbol { get; set; }
}