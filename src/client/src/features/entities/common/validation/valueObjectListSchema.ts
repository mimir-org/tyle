import * as yup from "yup";

export const valueObjectListSchema = (errorText: string) =>
  yup.array().of(
    yup.object().shape({
      value: yup.string().required(errorText),
    }),
  );
