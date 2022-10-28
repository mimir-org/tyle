import { RdsLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryrds";

export const rdsApi = {
  getRds() {
    return apiClient.get<RdsLibCm[]>(_basePath).then((r) => r.data);
  },
};
