namespace TypeLibrary.Data.Profiles;

public class BlockTerminalProfile : Profile
{
    public BlockTerminalProfile()
    {
        CreateMap<BlockTerminalTypeReference, TerminalTypeReferenceView>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TerminalId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Terminal.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Terminal.Description))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.Terminal.CreatedOn))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Terminal.CreatedBy))
            .ForMember(dest => dest.ContributedBy, opt => opt.MapFrom(src => src.Terminal.ContributedBy))
            .ForMember(dest => dest.LastUpdateOn, opt => opt.MapFrom(src => src.Terminal.LastUpdateOn))
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Terminal.Classifiers.Select(x => x.Classifier)))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Terminal.Purpose))
            .ForMember(dest => dest.Notation, opt => opt.MapFrom(src => src.Terminal.Notation))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Terminal.Symbol))
            .ForMember(dest => dest.Aspect, opt => opt.MapFrom(src => src.Terminal.Aspect))
            .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => src.Terminal.Medium))
            .ForMember(dest => dest.Qualifier, opt => opt.MapFrom(src => src.Terminal.Qualifier))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Terminal.TerminalAttributes));
    }
}