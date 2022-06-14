import { SymbolLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "librarysymbol";

export const apiSymbol = {
  getSymbols() {
    return apiClient.get<SymbolLibCm[]>(_basePath).then((r) => r.data);
  },
};
