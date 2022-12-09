import { ApprovalDataCm, State, TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "librarytransport";

export const transportApi = {
  getTransports() {
    return apiClient.get<TransportLibCm[]>(_basePath).then((r) => r.data);
  },
  getTransport(id?: string) {
    return apiClient.get<TransportLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postTransport(item: TransportLibAm) {
    return apiClient.post<TransportLibCm>(_basePath, item).then((r) => r.data);
  },
  putTransport(item: TransportLibAm) {
    return apiClient.put<TransportLibCm>(_basePath, item).then((r) => r.data);
  },
  patchTransportState(id: string, state: State) {
    return apiClient.patch<TransportLibCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  patchTransportStateReject(id: string) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/reject`).then((r) => r.data);
  },
};
