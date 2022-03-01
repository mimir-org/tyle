import { apiClient } from "../apiClient";
import { UnitLibAm } from "../../../models/typeLibrary/application/unitLibAm";
import { UnitLibCm } from "../../../models/typeLibrary/client/unitLibCm";

const _basePath = "libraryunit";

export const apiUnit = {
  getUnits() {
    return apiClient.get<UnitLibCm[]>(_basePath).then((r) => r.data);
  },
  postUnit(item: UnitLibAm) {
    return apiClient.post<UnitLibCm>(_basePath, item).then((r) => r.data);
  },
  putUnit(id: string, item: UnitLibAm) {
    return apiClient.put<UnitLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
};
