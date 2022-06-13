import { TerminalLibCm } from "../../../models/tyle/client/terminalLibCm";
import { apiClient } from "../apiClient";

const _basePath = "libraryterminal";

export const apiRds = {
  getTerminals() {
    return apiClient.get<TerminalLibCm[]>(_basePath).then((r) => r.data);
  },
};
