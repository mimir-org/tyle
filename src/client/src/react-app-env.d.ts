/// <reference types="react-scripts" />

interface TypeLibraryEnv {
  API_BASE_URL?: string;
  TYLE_VERSION?: string;
  SOCKET_BASE_URL?: string;
  APP_ID?: string;
  CLIENT_ID?: string;
  TENANT_ID?: string;
  COMPANY?: string;
  APP_INSIGHTS_CONNECTION_STRING?: string;
}

// eslint-disable-next-line no-var
declare var __TYPELIBRARY_ENV: TypeLibraryEnv;
