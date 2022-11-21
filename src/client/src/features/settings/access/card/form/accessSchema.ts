import { YupShape } from "common/types/yupShape";
import { FormUserPermission } from "features/settings/access/card/form/types/formUserPermission";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const accessSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormUserPermission>>({
    userId: yup.string().required(t("settings.access.validation.userId.required")),
    companyId: yup.number().required(t("settings.access.validation.companyId.required")),
    permission: yup.object().required(t("settings.access.validation.permission.required")),
  });
