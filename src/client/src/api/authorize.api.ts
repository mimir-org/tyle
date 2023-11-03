import {
  MimirorgPermissionCm,
  MimirorgRoleCm,
  MimirorgUserPermissionAm,
  MimirorgUserRoleAm,
} from "@mimirorg/typelibrary-types";
import { apiClient } from "api/clients/apiClient";

const _basePath = "mimirorgauthorize";

export const authorizeApi = {
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
  postAddUserPermission(item: MimirorgUserPermissionAm) {
    return apiClient.post<boolean>(`${_basePath}/permission/add`, item).then((r) => r.data);
  },
  postRemoveUserPermission(item: MimirorgUserPermissionAm) {
    return apiClient.post<boolean>(`${_basePath}/permission/remove`, item).then((r) => r.data);
  },
};
