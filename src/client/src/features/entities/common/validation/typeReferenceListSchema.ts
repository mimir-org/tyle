import * as yup from "yup";

export const typeReferenceListSchema = (errorText: string) =>
  yup.array().of(
    yup.object().shape({
      name: yup.string().required(errorText),
    })
  );
