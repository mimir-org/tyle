import { TFunction } from "i18next";
import * as yup from "yup";

export const loginSchema = (t: TFunction<"translation">) =>
  yup.object({
    email: yup.string().email(t("login.validation.email.email")).required(t("login.validation.email.required")),
    password: yup.string().required(t("login.validation.password.required")),
    code: yup
      .string()
      .matches(/^[0-9]*$/, t("login.validation.code.matches"))
      .required(t("login.validation.code.required")),
  });