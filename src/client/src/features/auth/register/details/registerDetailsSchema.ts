import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const registerDetailsSchema = (t: TFunction<"translation">, companiesAreAvailable = false) => {
  const schema = yup.object<YupShape<MimirorgUserAm>>({
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
    companyId: yup.number().nullable().notRequired(),
  });

  if (companiesAreAvailable) {
    return schema.shape({
      companyId: yup
        .number()
        .min(1, t("register.details.validation.companyId.min"))
        .required(t("register.details.validation.companyId.required")),
    });
  }

  return schema;
};
