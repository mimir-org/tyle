import {
  MimirorgChangePasswordAm,
  MimirorgQrCodeCm,
  MimirorgUserAm,
  MimirorgUserCm,
  MimirorgVerifyAm,
} from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "mimirorguser";

export const apiUser = {
  getCurrentUser() {
    return apiClient.get<MimirorgUserCm>(_basePath).then((r) => r.data);
  },
  postUser(item: MimirorgUserAm) {
    return apiClient.post<MimirorgQrCodeCm>(_basePath, item).then((r) => r.data);
  },
  postVerification(item: MimirorgVerifyAm) {
    return apiClient.post<boolean>(`${_basePath}/verify`, item).then((r) => r.data);
  },
  postGenerateMfa(item: MimirorgVerifyAm) {
    return apiClient.post<MimirorgQrCodeCm>(`${_basePath}/2fa`, item).then((r) => r.data);
  },
  postChangePassword(item: MimirorgChangePasswordAm) {
    return apiClient.post<boolean>(`${_basePath}/password`, item).then((r) => r.data);
  },
  postGenerateChangePasswordSecret(email: string) {
    return apiClient.post(`${_basePath}/password/secret/create`, email).then((r) => r.data);
  },
};
