import { SymbolLibCm } from "../../../models/tyle/client/symbolLibCm";
import { apiClient } from "../apiClient";

const _basePath = "librarysymbol";

export const apiSymbol = {
  getSymbols() {
    return apiClient.get<SymbolLibCm[]>(_basePath).then((r) => r.data);
  },
};
