import { TFunction } from "i18next";
import * as yup from "yup";

export const registerDetailsSchema = (t: TFunction<"translation">) => {
  const schema = yup.object({
    email: yup
      .string()
      .email(t("register.details.validation.email.email"))
      .required(t("register.details.validation.email.required")),
    password: yup
      .string()
      .min(10, t("register.details.validation.password.min"))
      .required(t("register.details.validation.password.required")),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref("password"), undefined], t("register.details.validation.confirmPassword.match"))
      .required(t("register.details.validation.confirmPassword.required")),
    firstName: yup.string().required(t("register.details.validation.firstName.required")),
    lastName: yup.string().required(t("register.details.validation.lastName.required")),
    purpose: yup.string().nullable().notRequired(),
  });

  return schema;
};
