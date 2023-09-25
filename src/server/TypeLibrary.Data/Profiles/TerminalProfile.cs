namespace TypeLibrary.Data.Profiles;

public class TerminalProfile : Profile
{
    public TerminalProfile(IApplicationSettingsRepository settings, IHttpContextAccessor contextAccessor)
    {
        CreateMap<TerminalType, TerminalTypeView>()
            .ForMember(dest => dest.Classifiers, opt => opt.MapFrom(src => src.Classifiers.Select(x => x.Classifier)))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.TerminalAttributes));

        CreateMap<TerminalTypeView, ApprovalCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 0))
            //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => ""))
            //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            //.ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.ToString()))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => "Terminal"))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}