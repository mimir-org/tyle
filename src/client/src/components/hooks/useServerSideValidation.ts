import { useEffect } from "react";
import { Path, UseFormSetError } from "react-hook-form";
import { getGeneralValidationError } from "../../data/helpers/getGeneralValidationError";

/**
 * Combines errors received through axios and react query with the error structure in react-hook-form
 * @param error to be transformed into shared format
 * @param setError function with puts a given error into a structure where it can be consumed
 */
export function useServerSideValidation<T>(error: unknown, setError: UseFormSetError<T>) {
  useEffect(() => {
    if (error) {
      const validationError = getGeneralValidationError<T>(error);
      validationError.errors && addServerErrors<T>(validationError.errors, setError);
    }
  }, [error, setError]);
}

function addServerErrors<T>(errors: Record<keyof T, string[]>, setError: UseFormSetError<T>) {
  Object.keys(errors).forEach((propertyName) => {
    setError(propertyName as Path<T>, {
      type: "Server",
      message: errors[propertyName as keyof T].join(". "),
    });
  });
}
