import { RdsLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "libraryrds";

export const apiRds = {
  getRds() {
    return apiClient.get<RdsLibCm[]>(_basePath).then((r) => r.data);
  },
};
