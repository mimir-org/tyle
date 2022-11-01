import axios, { AxiosError } from "axios";

/**
 * Takes an unknown server error model and returns a more general type
 *
 * @param error unknown type
 * @returns a more general formatted error, null if no error is supplied
 */
export const parseValidationStateFromServer = <T>(error: unknown): ValidationState<T> | null => {
  if (!error) return null;

  // internal axios bug causes eslint warning
  // eslint-disable-next-line import/no-named-as-default-member
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

interface ValidationState<T> {
  message: string;
  errors?: Record<keyof T, string[]>;
}

interface ServerValidationError<T> {
  errors: Record<keyof T, string[]>;
  type: string;
  title: string;
  status: number;
  traceId: string;
}
