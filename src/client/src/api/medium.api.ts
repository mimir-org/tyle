import { RdlMedium } from "common/types/terminals/rdlMedium";
import { RdlMediumRequest } from "common/types/terminals/rdlMediumRequest";
import { apiClient } from "api/clients/apiClient";

const _basePath = "media";

export const mediumApi = {
  getMedia() {
    return apiClient.get<RdlMedium[]>(_basePath).then((r) => r.data);
  },
  getMedium(id: number) {
    return apiClient.get<RdlMedium>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postMedium(item: RdlMediumRequest) {
    return apiClient.post<RdlMedium>(`${_basePath}`, item).then((r) => r.data);
  },
  deleteMedium(id: number) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
