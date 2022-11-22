import { MimirorgTokenCm } from "@mimirorg/typelibrary-types";

const localStorageKey = "tyleToken";

const getToken = () => {
  const token = window.localStorage.getItem(localStorageKey);
  if (token) return JSON.parse(token) as MimirorgTokenCm;
  return null;
};

const setToken = (token: MimirorgTokenCm) => window.localStorage.setItem(localStorageKey, JSON.stringify(token));

const removeToken = () => window.localStorage.removeItem(localStorageKey);

export { getToken, setToken, removeToken };
