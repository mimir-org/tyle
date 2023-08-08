export const isProduction = import.meta.env.NODE_ENV !== "development";

const assignValue = (key: keyof TypeLibraryEnv) =>
  isProduction ? window.__TYPELIBRARY_ENV[key] : import.meta.env[`VITE_${key}`];

const assignVersion = (key: keyof TypeLibraryEnv) =>
  isProduction ? window.__TYPELIBRARY_ENV[key] : import.meta.env[`VITE_${key}`];

const config: TypeLibraryEnv = {
  API_BASE_URL: assignValue("API_BASE_URL"),
  TYLE_VERSION: assignVersion("TYLE_VERSION"),
};

export default config;
