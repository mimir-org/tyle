import * as yup from "yup";

export const recoverDetailsSchema = () =>
  yup.object({
    email: yup
      .string()
      .email("Please ensure that the email is formatted correctly")
      .required("Please specify an email"),
  });
