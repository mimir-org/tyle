import { TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";
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
  putTransport(id: string, item: TransportLibAm) {
    return apiClient.put<TransportLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  deleteTransport(id: string) {
    return apiClient.delete<boolean>(`${_basePath}/${id}`).then((r) => r.data);
  },
};
