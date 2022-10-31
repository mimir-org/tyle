import { PurposeLibCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "librarypurpose";

export const purposeApi = {
  getPurposes() {
    return apiClient.get<PurposeLibCm[]>(_basePath).then((r) => r.data);
  },
};
