import { ApprovalDataCm, State, UnitLibAm, UnitLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryunit";

export const unitApi = {
  getUnits() {
    return apiClient.get<UnitLibCm[]>(_basePath).then((r) => r.data);
  },
  getUnit(id?: string) {
    return apiClient.get<UnitLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postUnit(item: UnitLibAm) {
    return apiClient.post<UnitLibCm>(`${_basePath}`, item).then((r) => r.data);
  },
  putUnit(item: UnitLibAm, id?: string) {
    return apiClient.put<UnitLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchUnitState(id: string, state: State) {
    return apiClient.patch<UnitLibCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  patchUnitStateReject(id: string) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/reject`).then((r) => r.data);
  },
};
