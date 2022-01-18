import { ApiError, GetBadResponseData, HttpResponse } from ".";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const GetApiErrorForBadRequest = (response: HttpResponse<any>, key: string): ApiError => {
  const data = GetBadResponseData(response);

  return {
    key,
    errorMessage: data?.title,
    errorData: data,
  };
};

export default GetApiErrorForBadRequest;
