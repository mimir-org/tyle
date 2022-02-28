import axios from "axios";
import Config from "../../models/Config";

export const apiClient = axios.create({
  baseURL: Config.API_BASE_URL,
});
