using Tyle.Application.Attributes.Views;

namespace Tyle.Application.Common.Views;

public class AttributeTypeReferenceView : AttributeTypeView
{
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
}