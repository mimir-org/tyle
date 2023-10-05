using System.ComponentModel.DataAnnotations;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;

namespace Tyle.Application.Attributes.Requests;

public class RdlUnitRequest : RdlObjectRequest
{
    [MaxLength(StringLengthConstants.UnitSymbolLength)]
    public string? Symbol { get; set; }
}