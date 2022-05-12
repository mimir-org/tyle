import { apiClient } from "../apiClient";
import { TerminalLibCm } from "../../../models/tyle/client/terminalLibCm";
import { TerminalLibAm } from "../../../models/tyle/application/terminalLibAm";

const _basePath = "libraryterminal";

export const apiRds = {
  getTerminals() {
    return apiClient.get<TerminalLibCm[]>(_basePath).then((r) => r.data);
  },
  postTerminal(item: TerminalLibAm) {
    return apiClient.post<TerminalLibCm>(_basePath, item).then((r) => r.data);
  },
};
