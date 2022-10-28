import { UnitLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryunit";

export const unitApi = {
  getUnits() {
    return apiClient.get<UnitLibCm[]>(_basePath).then((r) => r.data);
  },
};
