const isProduction = process.env.NODE_ENV !== "development";

const assignValue = (key: keyof TypeLibraryEnv) =>
  isProduction
    ? window.__TYPELIBRARY_ENV[key]
    : process.env[`REACT_APP_${key}`];

const config: TypeLibraryEnv = {
  API_BASE_URL: assignValue("API_BASE_URL"),
  SOCKET_BASE_URL: assignValue("SOCKET_BASE_URL"),
  APP_ID: assignValue("APP_ID"),
  CLIENT_ID: assignValue("CLIENT_ID"),
  TENANT_ID: assignValue("TENANT_ID"),
  APP_INSIGHTS_CONNECTION_STRING: assignValue("APP_INSIGHTS_CONNECTION_STRING"),
  COMPANY: assignValue("COMPANY"),
};

export default config;
