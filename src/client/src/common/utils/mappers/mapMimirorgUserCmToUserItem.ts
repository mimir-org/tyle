import { MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { UserItem } from "common/types/userItem";
import { getOptionsFromEnum, Option } from "common/utils/getOptionsFromEnum";

export const mapMimirorgUserCmToUserItem = (user: MimirorgUserCm): UserItem => {
  const permissionOptions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const permissionsMap: { [key: string]: Option<MimirorgPermission> } = {};

  Object.keys(user.permissions).forEach((companyId) => {
    const permissionForCompany = user.permissions[Number(companyId)];
    const permissionLabel = permissionOptions.find((x) => x.value == permissionForCompany)?.label;

    if (permissionLabel) {
      permissionsMap[companyId] = { value: permissionForCompany, label: permissionLabel };
    }
  });

  return {
    id: user.id,
    name: `${user.firstName} ${user.lastName}`,
    email: user.email,
    purpose: user.purpose,
    permissions: permissionsMap,
    company: {
      id: user.companyId,
      name: user.companyName,
    },
  };
};
