/*using AutoMapper;

namespace TypeLibrary.Data.Profiles;

public class LogProfile : Profile
{
    public LogProfile()
    {
        CreateMap<LogLibDm, LogLibCm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.ObjectId))
            .ForMember(dest => dest.ObjectFirstVersionId, opt => opt.MapFrom(src => src.ObjectFirstVersionId))
            .ForMember(dest => dest.ObjectVersion, opt => opt.MapFrom(src => src.ObjectVersion))
            .ForMember(dest => dest.ObjectType, opt => opt.MapFrom(src => src.ObjectType))
            .ForMember(dest => dest.ObjectName, opt => opt.MapFrom(src => src.ObjectName))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.LogType, opt => opt.MapFrom(src => src.LogType))
            .ForMember(dest => dest.LogTypeValue, opt => opt.MapFrom(src => src.LogTypeValue));
    }
}*/