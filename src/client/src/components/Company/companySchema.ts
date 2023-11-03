import { TFunction } from "i18next";
import { YupShape } from "types/yupShape";
import * as yup from "yup";
import { FormMimirorgCompany } from "./CompanyForm.helpers";

export const companySchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormMimirorgCompany>>({
    name: yup.string().required(t("company.validation.name.required")),
    displayName: yup.string().required(t("company.validation.displayName.required")),
    description: yup.string(),
    domain: yup.string().required(t("company.validation.domain.required")),
    logo: yup.object().nullable().required(t("company.validation.logo.required")),
    homePage: yup.string(),
  });
