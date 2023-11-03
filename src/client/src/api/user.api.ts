import { apiClient } from "api/clients/apiClient";
import { ChangePasswordRequest } from "types/authentication/changePasswordRequest";
import { QrCodeView } from "types/authentication/qrCodeView";
import { UserRequest } from "types/authentication/userRequest";
import { UserView } from "types/authentication/userView";
import { VerifyRequest } from "types/authentication/verifyRequest";

const _basePath = "mimirorguser";

export const userApi = {
  getCurrentUser() {
    return apiClient.get<UserView>(_basePath).then((r) => r.data);
  },
  postUser(item: UserRequest) {
    return apiClient.post<QrCodeView>(_basePath, item).then((r) => r.data);
  },
  patchUser(item: UserRequest) {
    return apiClient.patch<UserView>(_basePath, item).then((r) => r.data);
  },
  postVerification(item: VerifyRequest) {
    return apiClient.post<boolean>(`${_basePath}/verify`, item).then((r) => r.data);
  },
  postGenerateMfa(item: VerifyRequest) {
    return apiClient.post<QrCodeView>(`${_basePath}/2fa`, item).then((r) => r.data);
  },
  postChangePassword(item: ChangePasswordRequest) {
    return apiClient.post<boolean>(`${_basePath}/password`, item).then((r) => r.data);
  },
  postGenerateChangePasswordSecret(email: string) {
    return apiClient
      .post(`${_basePath}/password/secret/create`, email, { headers: { "Content-Type": "application/json" } })
      .then((r) => r.data);
  },
};
