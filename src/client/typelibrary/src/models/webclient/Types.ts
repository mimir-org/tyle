export interface ApiError {
  key: string;
  errorMessage?: string;
  errorData?: BadRequestData;
}
export interface BadRequestData {
  title: string;
  items: BadRequestDataItem[];
}
export interface BadRequestDataItem {
  key: string;
  value: string;
}

export interface HttpResponse<T> extends Response {
  data?: T;
}

export const RequestInitDefault: RequestInit = {
  method: "get",
  cache: "no-store",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Methods": "DELETE, POST, GET, PUT, OPTIONS",
    "Access-Control-Allow-Origin": "http://localhost:3000",
    "Access-Control-Allow-Headers":
      "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With",
    Origin: "http://localhost:3000",
    "Cache-Control": "no-cache",
  },
};
