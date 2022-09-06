import { TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "libraryterminal";

export const apiTerminal = {
  getTerminals() {
    return apiClient.get<TerminalLibCm[]>(_basePath).then((r) => r.data);
  },
  getTerminal(id?: string) {
    return apiClient.get<TerminalLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postTerminal(item: TerminalLibAm) {
    return apiClient.post<TerminalLibCm>(_basePath, item).then((r) => r.data);
  },
  putTerminal(id: string, item: TerminalLibAm) {
    return apiClient.put<TerminalLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
};
