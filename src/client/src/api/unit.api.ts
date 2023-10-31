import { RdlUnit } from "common/types/attributes/rdlUnit";
import { RdlUnitRequest } from "common/types/attributes/rdlUnitRequest";
import { apiClient } from "api/clients/apiClient";

const _basePath = "units";

export const unitApi = {
  getUnits() {
    return apiClient.get<RdlUnit[]>(_basePath).then((r) => r.data);
  },
  getUnit(id: number) {
    return apiClient.get<RdlUnit>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postUnit(item: RdlUnitRequest) {
    return apiClient.post<RdlUnit>(`${_basePath}`, item).then((r) => r.data);
  },
  deleteUnit(id: number) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
