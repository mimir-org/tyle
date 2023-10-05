using TypeLibrary.Core.Blocks;
using TypeLibrary.Services.Blocks.Requests;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Services.Blocks;

public interface IBlockRepository : ITypeRepository<BlockType, BlockTypeRequest>
{
}