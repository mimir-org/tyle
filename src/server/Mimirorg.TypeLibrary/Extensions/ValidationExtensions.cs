using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.TypeLibrary.Extensions
{
    public static class ValidationExtensions
    {
        public static IEnumerable<ValidationResult> ValidatePermission(this MimirorgPermissionAm permission)
        {
            var permissions = ((MimirorgPermission[]) Enum.GetValues(typeof(MimirorgPermission))).Select(c => new MimirorgPermissionAm { Id = (int) c, Name = c.GetDisplayName() }).ToList();
            if (!permissions.Any(x => x.Id == permission.Id && x.Name == permission.Name))
                yield return new ValidationResult("There is no permission with current id or name", new[] { "Id", "Name" });
        }
    }
}