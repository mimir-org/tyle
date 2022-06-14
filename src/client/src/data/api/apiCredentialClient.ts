import axios from "axios";
import Config from "../../models/Config";

/**
 * Only used for authentication api endpoints, where cookies should be sent together with request
 */
export const apiCredentialClient = axios.create({
  baseURL: Config.API_BASE_URL,
  withCredentials: true,
});
