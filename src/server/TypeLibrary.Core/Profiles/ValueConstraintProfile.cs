using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Core.Profiles;

public class ValueConstraintProfile : Profile
{
    public ValueConstraintProfile()
    {
        CreateMap<ValueConstraint, ValueConstraintView>();
    }
}