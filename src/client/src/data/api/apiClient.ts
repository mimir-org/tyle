import axios from "axios";
import Config from "../../models/Config";
import { injectBearerToken } from "./interceptors/injectBearerToken";
import { refreshExpiredToken } from "./interceptors/refreshExpiredToken";

export const apiClient = axios.create({
  baseURL: Config.API_BASE_URL,
});

apiClient.interceptors.request.use(injectBearerToken);
apiClient.interceptors.request.use(refreshExpiredToken);
