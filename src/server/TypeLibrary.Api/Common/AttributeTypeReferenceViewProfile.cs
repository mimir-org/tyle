using AutoMapper;
using TypeLibrary.Core.Blocks;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Api.Common;

public class AttributeTypeReferenceViewProfile : Profile
{
    public AttributeTypeReferenceViewProfile()
    {
        CreateMap<TerminalAttributeTypeReference, AttributeTypeReferenceView>();

        CreateMap<BlockAttributeTypeReference, AttributeTypeReferenceView>();
    }
}