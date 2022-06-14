import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";
import { MimirorgTokenCm } from "../../../models/auth/client/mimirorgTokenCm";
import { apiCredentialClient } from "../apiCredentialClient";

const _basePath = "mimirorgauthenticate";

export const apiAuthenticate = {
  postLogin(item: MimirorgAuthenticateAm) {
    return apiCredentialClient.post<MimirorgTokenCm>(_basePath, item).then((r) => r.data);
  },
  postLoginSecret() {
    return apiCredentialClient.post<MimirorgTokenCm>(`${_basePath}/secret`).then((r) => r.data);
  },
};
