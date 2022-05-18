import { apiClient } from "../apiClient";
import { TransportLibCm } from "../../../models/tyle/client/transportLibCm";

const _basePath = "librarytransport";

export const apiTransport = {
  getTransports() {
    return apiClient.get<TransportLibCm[]>(_basePath).then((r) => r.data);
  },
  getTransport(id: string) {
    return apiClient.get<TransportLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
};
