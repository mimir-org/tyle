import axios, { AxiosError } from "axios";
import { ServerValidationError } from "../types/serverValidationError";
import { ValidationState } from "../types/validationState";

/**
 * Takes an unknown server error model and returns a more general type
 *
 * @param error unknown type
 * @returns a more general formatted error, null if no error is supplied
 */
export const parseValidationStateFromServer = <T>(error: unknown): ValidationState<T> | null => {
  if (!error) return null;

  if (axios.isAxiosError(error)) {
    return {
      message: "server",
      errors: (error as AxiosError<ServerValidationError<T>>).response?.data.errors,
    };
  }

  if (error instanceof Error) {
    return { message: error.message };
  }

  return { message: "Unspecified client error has occurred." };
};
