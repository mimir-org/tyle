import { TerminalTypeRequest } from "common/types/terminals/terminalTypeRequest";
import { TerminalView } from "common/types/terminals/terminalView";
import { apiClient } from "external/client/apiClient";

const _basePath = "terminals";

export const terminalApi = {
  getTerminals() {
    return apiClient.get<TerminalView[]>(_basePath).then((r) => r.data);
  },
  getTerminal(id: string) {
    return apiClient.get<TerminalView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postTerminal(item: TerminalTypeRequest) {
    return apiClient.post<TerminalView>(_basePath, item).then((r) => r.data);
  },
  putTerminal(item: TerminalTypeRequest, id: string) {
    return apiClient.put<TerminalView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  deleteTerminal(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
