import { apiClient } from "../apiClient";
import { RdsLibCm } from "../../../models/tyle/client/rdsLibCm";

const _basePath = "libraryrds";

export const apiRds = {
  getRds() {
    return apiClient.get<RdsLibCm[]>(_basePath).then((r) => r.data);
  },
};
