using Tyle.Core.Attributes;
using VDS.RDF;

namespace Tyle.Converters;

public interface IAttributeTypeConverter
{
    Task<IGraph> ConvertTypeToGraph(AttributeType type);
}
