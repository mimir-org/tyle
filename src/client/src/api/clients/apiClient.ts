import axios from "axios";
import config from "config";
import { injectBearerToken } from "./injectBearerToken";
import { refreshExpiredToken } from "./refreshExpiredToken";

export const apiClient = axios.create({
  baseURL: config.API_BASE_URL,
});

apiClient.interceptors.request.use(injectBearerToken);
apiClient.interceptors.request.use(refreshExpiredToken);
