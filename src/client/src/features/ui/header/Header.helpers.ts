import { MimirorgCompanyCm, MimirorgPermission } from "@mimirorg/typelibrary-types";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";

/**
 * Creates descriptions of user permissions by mapping the permissions
 * dictionary against a list of companies and a list of enum labels
 *
 * @param permissions
 * @param companies
 */
export const mapPermissionDescriptions = (
  permissions: { [index: number]: MimirorgPermission },
  companies: MimirorgCompanyCm[]
) => {
  const permissionsValueObjects = getOptionsFromEnum(MimirorgPermission);

  const permissionDescriptions = Object.keys(permissions)
    .filter((x) => x !== "0")
    .map((k) => {
      const permissionEnum = permissions[Number(k)];
      const companyLabel = companies.find((x) => x.id.toString() === k)?.displayName;
      const permissionLabel = permissionsValueObjects.find((x) => x.value == permissionEnum)?.label;

      return `${companyLabel}: ${permissionLabel}`;
    });

  return permissionDescriptions.sort((a, b) => a.localeCompare(b));
};
