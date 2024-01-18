import * as yup from "yup";

export const loginSchema = () =>
  yup.object({
    email: yup
      .string()
      .email("Please ensure that the e-mail is formatted correctly")
      .required("Please specify an e-mail"),
    password: yup.string().required("Please enter a password"),
    code: yup
      .string()
      .matches(/^[0-9]*$/, "Please ensure that there are only digits in your code")
      .required("Please specify an authentication code"),
  });
