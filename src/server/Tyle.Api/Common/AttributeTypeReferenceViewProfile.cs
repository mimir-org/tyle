using AutoMapper;
using Tyle.Core.Blocks;
using Tyle.Core.Terminals;

namespace Tyle.Api.Common;

public class AttributeTypeReferenceViewProfile : Profile
{
    public AttributeTypeReferenceViewProfile()
    {
        CreateMap<TerminalAttributeTypeReference, AttributeTypeReferenceView>();

        CreateMap<BlockAttributeTypeReference, AttributeTypeReferenceView>();
    }
}