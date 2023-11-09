import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { apiClient } from "./clients/apiClient";

const _basePath = "symbols";

export const symbolApi = {
  getSymbols() {
    return apiClient.get<EngineeringSymbol[]>(_basePath).then((r) => r.data);
  },
  getSymbol(id: number) {
    return apiClient.get<EngineeringSymbol>(`${_basePath}/${id}`).then((r) => r.data);
  },
};
