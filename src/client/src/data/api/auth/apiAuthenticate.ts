import { apiClient } from "../apiClient";
import { MimirorgTokenCm } from "../../../models/auth/client/mimirorgTokenCm";
import { MimirorgAuthenticateAm } from "../../../models/auth/application/mimirorgAuthenticateAm";

const _basePath = "mimirorgauthenticate";

export const apiAuthenticate = {
  postLogin(item: MimirorgAuthenticateAm) {
    return apiClient.post<MimirorgTokenCm>(_basePath, item, { withCredentials: true }).then((r) => r.data);
  },
};
