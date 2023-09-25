using Tyle.Application.Common.Requests;

namespace Tyle.Application.Attributes.Requests;

public class UnitReferenceRequest : ReferenceRequest
{
    public string? Symbol { get; set; }
}