using AutoMapper;
using TypeLibrary.Core.Blocks;

namespace TypeLibrary.Api.Blocks;

public class BlockViewProfile : Profile
{
    public BlockViewProfile()
    {
        CreateMap<BlockType, BlockView>()
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => x.Classifier)));
    }
}