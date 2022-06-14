import { AxiosRequestConfig } from "axios";
import { MimirorgTokenCm } from "../../../models/auth/client/mimirorgTokenCm";
import { getToken, setToken } from "../../../utils/token";
import { apiAuthenticate } from "../auth/apiAuthenticate";

/**
 * This middleware issues a post request against an endpoint which can read a refresh token from the client's cookies.
 * After validation of the refresh token the endpoint will return a new access token, and set a new refresh token in cookies.
 *
 * @param config
 */
export async function refreshExpiredToken(config: AxiosRequestConfig) {
  const token = getToken();

  if (token && isTokenExpired(token)) {
    const freshToken = await apiAuthenticate.postLoginSecret();
    freshToken && setToken(freshToken);
  }

  return config;
}

const isTokenExpired = (token: MimirorgTokenCm) => Date.now() >= new Date(token.validTo).getTime();
