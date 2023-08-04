import { parseValidationStateFromServer } from "common/hooks/server-validation/parseValidationStateFromServer";
import camelCase from "lodash/camelCase";
import { useEffect } from "react";
import { FieldValues, Path, UseFormSetError } from "react-hook-form";

/**
 * Shorthand hook for parsing a server error model and binding it against the react-hook-form error-structure
 *
 * @param serverErrors server errors to be parsed and placed in react-hook-form error-structure
 * @param setError puts a given error into a structure where it can be consumed later
 *
 * @example
 *   const { setError } = useForm<FormAttributeLib>();
 *   const mutation = useCreateAttribute();
 *   useServerValidation(mutation.error, setError);
 */
export const useServerValidation = <T extends FieldValues>(serverErrors: unknown, setError: UseFormSetError<T>) => {
  const validationState = parseValidationStateFromServer<T>(serverErrors);
  useServerErrorBinding(setError, validationState?.errors);
};

/**
 * If any errors present it will combine errors received with the error structure in react-hook-form
 *
 * @param setError puts a given error into a structure where it can be consumed later
 * @param errors server errors to be placed in react-hook-form error-structure
 */
const useServerErrorBinding = <T extends FieldValues>(
  setError: UseFormSetError<T>,
  errors?: Record<keyof T, string[]>,
) => {
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
};
