using AutoMapper;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Api.Terminals;

public class TerminalTypeViewProfile : Profile
{
    public TerminalTypeViewProfile()
    {
        CreateMap<TerminalType, TerminalTypeView>()
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => x.Classifier)));
    }
}