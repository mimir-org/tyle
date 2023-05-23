import { ApprovalDataCm, State, TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryterminal";

export const terminalApi = {
  getTerminals() {
    return apiClient.get<TerminalLibCm[]>(_basePath).then((r) => r.data);
  },
  getTerminal(id?: string) {
    return apiClient.get<TerminalLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postTerminal(item: TerminalLibAm) {
    return apiClient.post<TerminalLibCm>(_basePath, item).then((r) => r.data);
  },
  putTerminal(item: TerminalLibAm, id?: string) {
    return apiClient.put<TerminalLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchTerminalState(id: string, state: State) {
    return apiClient.patch<TerminalLibCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  patchhTerminalStateReject(id: string) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/reject`).then((r) => r.data);
  },
};
