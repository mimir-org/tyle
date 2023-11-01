import { MimirorgTokenCm } from "@mimirorg/typelibrary-types";
import { authenticateApi } from "api/authenticate.api";
import { getToken, setToken } from "api/token";
import { InternalAxiosRequestConfig } from "axios";

let expiredTokenRequest: Promise<MimirorgTokenCm> | null;
const resetExpiredTokenRequest = () => (expiredTokenRequest = null);

/**
 * This middleware issues a post request against an endpoint which can read a refresh token from the client's cookies.
 * After validation of the refresh token the endpoint will return a new access token, and set a new refresh token in cookies.
 *
 * Makes a call to get the access token only once, even if multiple requests triggers it.
 * @see https://github.com/axios/axios/issues/450
 *
 * @param config
 */
export async function refreshExpiredToken(config: InternalAxiosRequestConfig) {
  const currentToken = getToken();

  if (!expiredTokenRequest && currentToken && isTokenExpired(currentToken)) {
    expiredTokenRequest = authenticateApi.postLoginSecret();
    expiredTokenRequest.then(resetExpiredTokenRequest, resetExpiredTokenRequest);
    expiredTokenRequest.then((freshToken) => {
      freshToken && setToken(freshToken);
    });
  }

  return config;
}

const isTokenExpired = (token: MimirorgTokenCm) => Date.now() >= new Date(token.validTo).getTime();
