import axios from "axios";
import Config from "../../models/Config";
import { getToken } from "../../utils/token";

export const apiClient = axios.create({
  baseURL: Config.API_BASE_URL,
});

apiClient.interceptors.request.use((config) => {
  const token = getToken();

  if (token?.secret && config.headers) {
    config.headers["Authorization"] = `Bearer ${token.secret}`;
  }

  return config;
});
