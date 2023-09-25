namespace TypeLibrary.Data.Terminals;

public class TerminalProfile : Profile
{
    public TerminalProfile(IAttributeService attributeService)
    {
        CreateMap<TerminalType, TerminalDao>()
            .ForMember(dest => dest.TerminalClassifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => new TerminalClassifierDao(src.Id, x.Id))))
            .ForMember(dest => dest.PurposeId, opt =>
            {
                opt.PreCondition(src => src.Purpose != null);
                opt.MapFrom(src => src.Purpose!.Id);
            })
            .ForMember(dest => dest.Purpose, opt => opt.Ignore())
            .ForMember(dest => dest.Aspect, opt =>
            {
                opt.PreCondition(src => src.Aspect != null);
                opt.MapFrom(src => src.Aspect.ToString());
            })
            .ForMember(dest => dest.MediumId, opt =>
            {
                opt.PreCondition(src => src.Medium != null);
                opt.MapFrom(src => src.Medium!.Id);
            })
            .ForMember(dest => dest.Medium, opt => opt.Ignore())
            .ForMember(dest => dest.Qualifier, opt => opt.MapFrom(src => src.Qualifier.ToString()))
            .ForMember(dest => dest.TerminalAttributes, opt => opt.MapFrom(src => src.Attributes.Select(x => new TerminalAttributeDao(src.Id, x.Attribute.Id, x.MinCount, x.MaxCount))));

        CreateMap<TerminalDao, TerminalType>()
            .ConstructUsing(src => new TerminalType(src.Name, src.Description, new User("", "")))
            .ForMember(dest => dest.ContributedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.TerminalClassifiers.Select(x => x.Classifier)))
            .ForMember(dest => dest.Aspect, opt =>
            {
                opt.PreCondition(src => src.Aspect != null);
                opt.MapFrom(src => Enum.Parse<Aspect>(src.Aspect!));
            })
            .ForMember(dest => dest.Qualifier, opt => opt.MapFrom(src => Enum.Parse<Direction>(src.Qualifier)))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.TerminalAttributes.Select(x => new AttributeTypeReference(attributeService.Get(x.AttributeId).Result!, x.MinCount, x.MaxCount))));
    }
}
