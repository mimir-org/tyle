import { MimirorgQrCodeCm, MimirorgUserAm, MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "mimirorguser";

export const apiUser = {
  getCurrentUser() {
    return apiClient.get<MimirorgUserCm>(_basePath).then((r) => r.data);
  },
  postUser(item: MimirorgUserAm) {
    return apiClient.post<MimirorgQrCodeCm>(_basePath, item).then((r) => r.data);
  },
};
