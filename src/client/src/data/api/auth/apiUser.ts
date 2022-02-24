import { apiClient } from "../apiClient";
import { MimirorgQrCodeCm } from "../../../models/auth/client/mimirorgQrCodeCm";
import { MimirorgUserAm } from "../../../models/auth/application/mimirorgUserAm";
import { MimirorgUserCm } from "../../../models/auth/client/mimirorgUserCm";

const _basePath = "mimirorguser";

export const apiUser = {
  getCurrentUser() {
    return apiClient.get<MimirorgUserCm>(_basePath).then((r) => r.data);
  },
  postUser(item: MimirorgUserAm) {
    return apiClient.post<MimirorgQrCodeCm>(_basePath, item).then((r) => r.data);
  },
};
