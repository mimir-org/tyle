using AutoMapper;
using TypeLibrary.Core.Blocks;

namespace TypeLibrary.Api.Blocks;

public class TerminalTypeReferenceViewProfile : Profile
{
    public TerminalTypeReferenceViewProfile()
    {
        CreateMap<BlockTerminalTypeReference, TerminalTypeReferenceView>();
    }
}