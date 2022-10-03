import { InterfaceLibAm, InterfaceLibCm, State } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "libraryinterface";

export const apiInterface = {
  getInterfaces() {
    return apiClient.get<InterfaceLibCm[]>(_basePath).then((r) => r.data);
  },
  getInterface(id?: string) {
    return apiClient.get<InterfaceLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postInterface(item: InterfaceLibAm) {
    return apiClient.post<InterfaceLibCm>(_basePath, item).then((r) => r.data);
  },
  putInterface(item: InterfaceLibAm) {
    return apiClient.put<InterfaceLibCm>(_basePath, item).then((r) => r.data);
  },
  patchInterfaceState(id: string, state: State) {
    return apiClient.patch<InterfaceLibCm>(`${_basePath}/state/${id}`, state).then((r) => r.data);
  },
};
