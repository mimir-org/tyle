import { State, TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "librarytransport";

export const apiTransport = {
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
    return apiClient.patch<TransportLibCm>(`${_basePath}/state/${id}`, state).then((r) => r.data);
  },
};
