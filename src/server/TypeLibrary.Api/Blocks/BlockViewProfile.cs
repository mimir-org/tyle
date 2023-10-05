using AutoMapper;
using Tyle.Core.Blocks;

namespace Tyle.Api.Blocks;

public class BlockViewProfile : Profile
{
    public BlockViewProfile()
    {
        CreateMap<BlockType, BlockView>()
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => x.Classifier)));
    }
}