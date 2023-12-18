import * as yup from "yup";

export const recoverPasswordSchema = () =>
  yup.object({
    email: yup.string().required(),
    password: yup
      .string()
      .min(10, "The password must be at least 10 characters long")
      .required("Please enter a password"),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref("password"), undefined], "The password does not match the one previously entered")
      .required("Please re-enter the password you have chosen"),
    code: yup.string().required(),
  });
