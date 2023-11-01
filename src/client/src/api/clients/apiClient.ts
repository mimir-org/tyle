import { injectBearerToken } from "api/clients/injectBearerToken";
import { refreshExpiredToken } from "api/clients/refreshExpiredToken";
import axios from "axios";
import config from "config";

export const apiClient = axios.create({
  baseURL: config.API_BASE_URL,
});

apiClient.interceptors.request.use(injectBearerToken);
apiClient.interceptors.request.use(refreshExpiredToken);
