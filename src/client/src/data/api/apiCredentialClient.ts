import axios from "axios";
import config from "../../utils/config";

/**
 * Only used for authentication api endpoints, where cookies should be sent together with request
 */
export const apiCredentialClient = axios.create({
  baseURL: config.API_BASE_URL,
  withCredentials: true,
});
