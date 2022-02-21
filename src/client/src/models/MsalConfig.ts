import { Configuration, RedirectRequest } from "@azure/msal-browser";
import Config from "./Config";

// Config object to be passed to Msal on creation
export const msalConfig: Configuration = {
  auth: {
    clientId: Config.CLIENT_ID ?? "",
    authority: "https://login.microsoftonline.com/" + Config.TENANT_ID,
    redirectUri: "/",
    postLogoutRedirectUri: "/",
  },
};

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest: RedirectRequest = {
  scopes: ["api://" + Config.APP_ID + "/access_as_user"],
};

// Add here the endpoints for MS Graph API services you would like to use.
export const graphConfig = {
  graphMeEndpoint: "https://graph.microsoft.com/v1.0/me",
};
