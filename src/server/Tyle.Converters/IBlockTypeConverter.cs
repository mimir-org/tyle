using Tyle.Core.Blocks;
using VDS.RDF;

namespace Tyle.Converters;

public interface IBlockTypeConverter
{
    Task<IGraph> ConvertTypeToGraph(BlockType type);
}