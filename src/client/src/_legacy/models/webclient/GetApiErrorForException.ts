import { ApiError } from "./index";

const GetApiErrorForException = (error: Error, key: string): ApiError => {
  return  {
    key,
    errorMessage: error?.message,
    errorData: undefined,
  }
}

export default GetApiErrorForException;