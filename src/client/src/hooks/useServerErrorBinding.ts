import { camelCase } from "lodash";
import { useEffect } from "react";
import { FieldValues, Path, UseFormSetError } from "react-hook-form";

/**
 * If any errors present it will combine errors received with the error structure in react-hook-form
 *
 * @param setError function with puts a given error into a structure where it can be consumed
 * @param errors to be placed in react-hook-form error-structure
 */
export function useValidationFromServer<T extends FieldValues>(
  setError: UseFormSetError<T>,
  errors?: Record<keyof T, string[]>
) {
  useEffect(() => {
    if (errors) {
      Object.keys(errors).forEach((propertyName) => {
        setError(camelCase(propertyName) as Path<T>, {
          type: "server",
          message: errors[propertyName as keyof T].join(". "),
        });
      });
    }
  }, [errors, setError]);
}
