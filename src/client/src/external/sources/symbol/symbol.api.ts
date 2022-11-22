import { SymbolLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "librarysymbol";

export const symbolApi = {
  getSymbols() {
    return apiClient.get<SymbolLibCm[]>(_basePath).then((r) => r.data);
  },
};
