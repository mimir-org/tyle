using Tyle.Core.Terminals;
using VDS.RDF;

namespace Tyle.Converters;

public interface ITerminalTypeConverter
{
    Task<IGraph> ConvertTypeToGraph(TerminalType type);
}