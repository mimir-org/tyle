import { UnitLibCm } from "../../../models/tyle/client/unitLibCm";
import { apiClient } from "../apiClient";

const _basePath = "libraryunit";

export const apiUnit = {
  getUnits() {
    return apiClient.get<UnitLibCm[]>(_basePath).then((r) => r.data);
  },
};
