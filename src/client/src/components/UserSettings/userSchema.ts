import * as yup from "yup";

export const userSchema = () =>
  yup.object({
    firstName: yup.string().required("Please enter a first name"),
    lastName: yup.string().required("Please enter a last name"),
    email: yup.string().required(),
    password: yup.string().required(),
    confirmPassword: yup.string().required(),
    purpose: yup.string().required(),
  });
