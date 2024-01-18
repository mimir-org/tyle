import { TokenView } from "types/authentication/tokenView";

const localStorageKey = "tyleToken";

const getToken = () => {
  const token = window.localStorage.getItem(localStorageKey);
  if (token) return JSON.parse(token) as TokenView;
  return null;
};

const setToken = (token: TokenView) => window.localStorage.setItem(localStorageKey, JSON.stringify(token));

const removeToken = () => window.localStorage.removeItem(localStorageKey);

export { getToken, removeToken, setToken };
