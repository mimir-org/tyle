import { msalInstance } from "../../../index";
import { loginRequest } from "../../../models/MsalConfig";
import { HttpResponse, RequestInitDefault } from "./index";
import { TextResources } from "../../../assets/text";

export const Token = async () => {
  const account = msalInstance.getActiveAccount();

  if (!account) {
    throw Error(TextResources.Error_NoActiveAccount);
  }

  const response = await msalInstance.acquireTokenSilent({
    ...loginRequest,
    account: account,
  });

  return `Bearer ${response.accessToken}`;
};

export async function http<T>(request: RequestInfo): Promise<HttpResponse<T>> {
  const response = await fetch(request) as HttpResponse<T>;

  if (!isValidStatus(response.status)) {
    throw new Error(errorMessage(response));
  }

  if (hasContent(response)) {
    response.data = await response.json();
  }

  return response;
}

const isValidStatus = (status: number) => (status >= 200 && status < 300) || status === 400;
const hasContent = <T>(response: HttpResponse<T>) => response.status !== 204;

const errorMessage = <T>(response: HttpResponse<T>) => {
  if (response.status >= 401 && response.status <= 403) {
    return TextResources.Error_Forbidden;
  }

  if (response.status >= 500) {
    return TextResources.Error_Server;
  }

  return TextResources.Error_ServerUnavailable;
};

export async function get<T>(path: string, args: RequestInit = { method: "get" }): Promise<HttpResponse<T>> {
  const token = await Token();
  const req = { ...RequestInitDefault, ...args };
  req.headers = {...req.headers, ...{"Authorization": token}};
  return http<T>(new Request(path, req));
}

export async function post<T>(
  path: string,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  body: any,
  args: RequestInit = { method: "post", body: JSON.stringify(body) }
): Promise<HttpResponse<T>> {
  const token = await Token();
  const req = { ...RequestInitDefault, ...args };
  req.headers = {...req.headers, ...{"Authorization": token}};
  return http<T>(new Request(path, req));
}

export async function put<T>(
  path: string,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  body: any,
  args: RequestInit = { method: "put", body: JSON.stringify(body) }
): Promise<HttpResponse<T>> {
  const token = await Token();
  const req = { ...RequestInitDefault, ...args };
  req.headers = {...req.headers, ...{"Authorization": token}};
  return http<T>(new Request(path, req));
}
