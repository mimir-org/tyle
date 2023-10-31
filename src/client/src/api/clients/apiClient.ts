import axios from "axios";
import config from "common/utils/config";
import { injectBearerToken } from "api/clients/injectBearerToken";
import { refreshExpiredToken } from "api/clients/refreshExpiredToken";

export const apiClient = axios.create({
  baseURL: config.API_BASE_URL,
});

apiClient.interceptors.request.use(injectBearerToken);
apiClient.interceptors.request.use(refreshExpiredToken);
