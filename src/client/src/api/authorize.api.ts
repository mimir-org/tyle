import { apiClient } from "api/clients/apiClient";
import { RoleView } from "types/authentication/roleView";
import { UserRoleRequest } from "types/authentication/userRoleRequest";

const _basePath = "mimirorgauthorize";

export const authorizeApi = {
  getRoles() {
    return apiClient.get<RoleView[]>(`${_basePath}/role`).then((r) => r.data);
  },
  postAddUserRole(item: UserRoleRequest) {
    return apiClient.post<boolean>(`${_basePath}/role/add`, item).then((r) => r.data);
  },
  postRemoveUserRole(item: UserRoleRequest) {
    return apiClient.post<boolean>(`${_basePath}/role/remove`, item).then((r) => r.data);
  },
  putUpdateUserRole(item: UserRoleRequest) {
    return apiClient.put<boolean>(`${_basePath}/role/update`, item).then((r) => r.data);
  },
};
