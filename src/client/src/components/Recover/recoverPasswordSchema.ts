import { TFunction } from "i18next";
import * as yup from "yup";

export const recoverPasswordSchema = (t: TFunction<"translation">) =>
  yup.object({
    email: yup.string().required(),
    password: yup
      .string()
      .min(10, t("recover.password.validation.password.min"))
      .required(t("recover.password.validation.password.required")),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref("password"), undefined], t("recover.password.validation.confirmPassword.match"))
      .required(t("recover.password.validation.confirmPassword.required")),
    code: yup.string().required(),
  });
