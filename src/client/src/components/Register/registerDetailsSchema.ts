import * as yup from "yup";

export const registerDetailsSchema = () => {
  const schema = yup.object({
    email: yup
      .string()
      .email("Please ensure that the e-mail is formatted correctly")
      .required("Please specify an e-mail"),
    password: yup
      .string()
      .min(10, "The password must be at least 10 characters long")
      .required("Please enter a password"),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref("password"), undefined], "The password does not match the one previously entered")
      .required("Please re-enter the password you have chosen"),
    firstName: yup.string().required("Please enter your first name"),
    lastName: yup.string().required("Please enter your last name"),
    purpose: yup.string().required("Please enter a purpose"),
  });

  return schema;
};
