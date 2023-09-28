using System.ComponentModel.DataAnnotations;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Common.Requests;

namespace TypeLibrary.Services.Attributes.Requests;

public class RdlUnitRequest : RdlObjectRequest
{
    [MaxLength(StringLengthConstants.UnitSymbolLength)]
    public string? Symbol { get; set; }
}