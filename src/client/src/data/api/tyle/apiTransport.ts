import { TransportLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "librarytransport";

export const apiTransport = {
  getTransports() {
    return apiClient.get<TransportLibCm[]>(_basePath).then((r) => r.data);
  },
  getTransport(id: string) {
    return apiClient.get<TransportLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
};
