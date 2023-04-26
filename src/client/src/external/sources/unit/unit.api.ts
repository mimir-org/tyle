import { UnitLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryunit";

export const unitApi = {
  getUnits() {
    return apiClient.get<UnitLibCm[]>(_basePath).then((r) => r.data);
  },
  getUnit(id?: string) {
    return apiClient.get<UnitLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postUnit(item: UnitLibCm) {
    return apiClient.post<UnitLibCm>(_basePath, item).then((r) => r.data);
  },
};
