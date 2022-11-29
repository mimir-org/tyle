using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using TypeScriptBuilder;

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

        public static IEnumerable<MimirorgPermissionCm> FromPermissionEnum()
        {
            var permissionValues = Enum.GetValues<MimirorgPermission>();
            foreach (var permission in permissionValues)
            {
                yield return new MimirorgPermissionCm(permission);
            }
        }
    }
}