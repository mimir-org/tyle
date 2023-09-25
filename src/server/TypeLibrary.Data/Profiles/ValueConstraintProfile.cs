namespace TypeLibrary.Data.Profiles;

public class ValueConstraintProfile : Profile
{
    public ValueConstraintProfile()
    {
        CreateMap<ValueConstraint, ValueConstraintView>();
    }
}