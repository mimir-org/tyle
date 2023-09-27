using AutoMapper;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Api.Terminals;

public class TerminalViewProfile : Profile
{
    public TerminalViewProfile()
    {
        CreateMap<TerminalType, TerminalView>()
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => x.Classifier)));
    }
}