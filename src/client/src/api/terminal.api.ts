import { apiClient } from "api/clients/apiClient";
import { State } from "types/common/state";
import { StateChangeRequest } from "types/common/stateChangeRequest";
import { TerminalTypeRequest } from "types/terminals/terminalTypeRequest";
import { TerminalView } from "types/terminals/terminalView";

const _basePath = "terminals";

export const terminalApi = {
  getTerminals() {
    return apiClient.get<TerminalView[]>(_basePath).then((r) => r.data);
  },
  getTerminalsByState(state: State) {
    return apiClient.get<TerminalView[]>(`${_basePath}?state=${state}`).then((r) => r.data);
  },
  getTerminal(id?: string) {
    return apiClient.get<TerminalView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postTerminal(item: TerminalTypeRequest) {
    return apiClient.post<TerminalView>(_basePath, item).then((r) => r.data);
  },
  putTerminal(id: string, item: TerminalTypeRequest) {
    return apiClient.put<TerminalView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchTerminalState(id: string, item: StateChangeRequest) {
    return apiClient.patch(`${_basePath}/${id}/state`, item).then((r) => r.data);
  },
  deleteTerminal(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
