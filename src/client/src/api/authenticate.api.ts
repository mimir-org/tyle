import { MimirorgAuthenticateAm, MimirorgTokenCm } from "@mimirorg/typelibrary-types";
import { apiCredentialClient } from "api/clients/apiCredentialClient";

const _basePath = "mimirorgauthenticate";

export const authenticateApi = {
  postLogin(item: MimirorgAuthenticateAm) {
    return apiCredentialClient.post<MimirorgTokenCm>(_basePath, item).then((r) => r.data);
  },
  postLoginSecret() {
    return apiCredentialClient.post<MimirorgTokenCm>(`${_basePath}/secret`).then((r) => r.data);
  },
  postLogout() {
    return apiCredentialClient.post<boolean>(`${_basePath}/logout`).then((r) => r.data);
  },
};
