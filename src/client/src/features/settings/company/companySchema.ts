import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { FormMimirorgCompany } from "./CreateCompanyForm.helpers";

export const companySchema = (t: TFunction<"translation">) =>
    yup.object<YupShape<FormMimirorgCompany>>({
        name: yup.string().required(t("createCompany.validation.name.required")),
        displayName: yup.string().required(t("createCompany.validation.displayName.required")),
        description: yup.string(),
        domain: yup.string().required(t("createCompany.validation.domain.required")),
        logo: yup.object(),
        homePage: yup.string(),
    });
