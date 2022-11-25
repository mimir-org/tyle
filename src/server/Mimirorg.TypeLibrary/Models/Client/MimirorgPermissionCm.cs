using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class MimirorgPermissionCm
    {
        public int Id { get; }
        public string Name { get; }

        public MimirorgPermissionCm()
        {
        }

        private MimirorgPermissionCm(MimirorgPermission enumValue)
        {
            Id = (int) enumValue;
            Name = enumValue.GetDisplayName();
        }

        public static ICollection<MimirorgPermissionCm> FromPermissionEnum()
        {
            var permissionValues = Enum.GetValues<MimirorgPermission>();
            return permissionValues.Select(x => new MimirorgPermissionCm(x)).ToList();
        }
    }
}