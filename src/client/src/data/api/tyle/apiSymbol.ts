import { apiClient } from "../apiClient";
import { SymbolLibCm } from "../../../models/tyle/client/symbolLibCm";
import { SymbolLibAm } from "../../../models/tyle/application/symbolLibAm";

const _basePath = "librarysymbol";

export const apiSymbol = {
  getSymbols() {
    return apiClient.get<SymbolLibCm[]>(_basePath).then((r) => r.data);
  },
  postSymbol(item: SymbolLibAm) {
    return apiClient.post<SymbolLibCm>(_basePath, item).then((r) => r.data);
  },
};
