using Tyle.Application.Blocks.Requests;
using Tyle.Application.Common;
using Tyle.Core.Blocks;

namespace Tyle.Application.Blocks;

public interface IBlockRepository : ITypeRepository<BlockType, BlockTypeRequest>
{
}