import { ApprovalDataCm, QuantityDatumLibAm, QuantityDatumLibCm, State } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryquantitydatum";

export const quantityDatum = {
  getQuantityDatums() {
    return apiClient.get<QuantityDatumLibCm[]>(_basePath).then((r) => r.data);
  },
  getQuantityDatum(id?: string) {
    return apiClient.get<QuantityDatumLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postQuantityDatum(item: QuantityDatumLibAm) {
    return apiClient.post<QuantityDatumLibCm>(_basePath, item).then((r) => r.data);
  },
  putQuantityDatum(item: QuantityDatumLibAm, id?: string) {
    return apiClient.put<QuantityDatumLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchDatumState(id: string, state: State) {
    return apiClient.patch<QuantityDatumLibCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  patchDatumStateReject(id: string) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/reject`).then((r) => r.data);
  },
};
