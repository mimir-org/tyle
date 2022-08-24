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

        public static IEnumerable<ValidationResult> ValidateAttribute(this AttributeLibAm attribute)
        {
            if (attribute.Select == Select.None && (attribute.SelectValues != null && attribute.SelectValues.Any()))
                yield return new ValidationResult($"There should not be any values in {nameof(attribute.SelectValues)}, when Select is different from SingleSelect or MultiSelect", attribute.SelectValues);

            if (attribute.Select != Select.None && (attribute.SelectValues == null || !attribute.SelectValues.Any()))
                yield return new ValidationResult($"There should values in {nameof(attribute.SelectValues)}, when Select is SingleSelect or MultiSelect", attribute.SelectValues);
        }
    }
}