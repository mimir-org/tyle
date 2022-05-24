export const isProduction = process.env.NODE_ENV !== "development";

const assignValue = (key: keyof TypeLibraryEnv) =>
  isProduction ? window.__TYPELIBRARY_ENV[key] : process.env[`REACT_APP_${key}`];

const config: TypeLibraryEnv = {
  API_BASE_URL: assignValue("API_BASE_URL")
};

export default config;
