import axios from "axios";
import config from "../../common/utils/config";
import { injectBearerToken } from "./interceptors/injectBearerToken";
import { refreshExpiredToken } from "./interceptors/refreshExpiredToken";

export const apiClient = axios.create({
  baseURL: config.API_BASE_URL,
});

apiClient.interceptors.request.use(injectBearerToken);
apiClient.interceptors.request.use(refreshExpiredToken);
