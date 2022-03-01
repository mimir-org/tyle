import { ValidationError } from "../types/validationError";
import axios, { AxiosError } from "axios";
import { ServerValidationError } from "../types/serverValidationError";

/**
 * Takes an unknown error and returns a more general type
 * @param error unknown type
 * @return a general validation error
 */
export function getGeneralValidationError<T>(error: unknown): ValidationError<T> {
  return axios.isAxiosError(error)
    ? { message: "server", errors: (error as AxiosError<ServerValidationError<T>>).response?.data.errors }
    : { message: "client error occurred" };
}
