using AutoMapper;
using Tyle.Core.Blocks;

namespace Tyle.Api.Blocks;

public class TerminalTypeReferenceViewProfile : Profile
{
    public TerminalTypeReferenceViewProfile()
    {
        CreateMap<BlockTerminalTypeReference, TerminalTypeReferenceView>();
    }
}