import { apiCredentialClient } from "api/clients/apiCredentialClient";
import { AuthenticateRequest } from "types/authentication/authenticateRequest";
import { TokenView } from "types/authentication/tokenView";

const _basePath = "mimirorgauthenticate";

export const authenticateApi = {
  postLogin(item: AuthenticateRequest) {
    return apiCredentialClient.post<TokenView>(_basePath, item).then((r) => r.data);
  },
  postLoginSecret() {
    return apiCredentialClient.post<TokenView>(`${_basePath}/secret`).then((r) => r.data);
  },
  postLogout() {
    return apiCredentialClient.post<boolean>(`${_basePath}/logout`).then((r) => r.data);
  },
};
