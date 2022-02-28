import { apiClient } from "../apiClient";
import { MimirorgRoleCm } from "../../../models/auth/client/mimirorgRoleCm";
import { MimirorgUserRoleAm } from "../../../models/auth/application/mimirorgUserRoleAm";
import { MimirorgPermissionCm } from "../../../models/auth/client/mimirorgPermissionCm";
import { MimirorgPermissionAm } from "../../../models/auth/application/mimirorgPermissionAm";

const _basePath = "mimirorgauthorize";

export const apiAuthorize = {
  getRoles() {
    return apiClient.get<MimirorgRoleCm[]>(`${_basePath}/role`).then((r) => r.data);
  },
  postAddUserRole(item: MimirorgUserRoleAm) {
    return apiClient.post<boolean>(`${_basePath}/role/add`, item).then((r) => r.data);
  },
  postRemoveUserRole(item: MimirorgUserRoleAm) {
    return apiClient.post<boolean>(`${_basePath}/role/remove`, item).then((r) => r.data);
  },
  getPermissions() {
    return apiClient.get<MimirorgPermissionCm>(`${_basePath}/permission`).then((r) => r.data);
  },
  postUserPermission(item: MimirorgPermissionAm) {
    return apiClient.post<boolean>(`${_basePath}/permission`, item).then((r) => r.data);
  },
};
