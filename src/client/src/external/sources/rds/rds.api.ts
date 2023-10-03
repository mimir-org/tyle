import { ApprovalDataCm, RdsLibCm, State, RdsLibAm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryrds";

export const rdsApi = {
  getAllRds() {
    return apiClient.get<RdsLibCm[]>(_basePath).then((r) => r.data);
  },
  getRds(id?: string) {
    return apiClient.get<RdsLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postRds(item: RdsLibAm) {
    return apiClient.post<RdsLibCm>(_basePath, item).then((r) => r.data);
  },
  putRds(item: RdsLibAm, id?: string) {
    return apiClient.put<RdsLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchRdsState(id: string, state: State) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  deleteRds(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
