import { RdlPurpose } from "common/types/common/rdlPurpose";
import { RdlPurposeRequest } from "common/types/common/rdlPurposeRequest";
import { apiClient } from "api/clients/apiClient";

const _basePath = "purposes";

export const purposeApi = {
  getPurposes() {
    return apiClient.get<RdlPurpose[]>(_basePath).then((r) => r.data);
  },
  getPurpose(id: number) {
    return apiClient.get<RdlPurpose>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postPurpose(item: RdlPurposeRequest) {
    return apiClient.post<RdlPurpose>(`${_basePath}`, item).then((r) => r.data);
  },
  deletePurpose(id: number) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
