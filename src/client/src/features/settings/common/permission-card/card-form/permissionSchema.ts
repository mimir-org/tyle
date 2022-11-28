import { YupShape } from "common/types/yupShape";
import { FormUserPermission } from "features/settings/common/permission-card/card-form/types/formUserPermission";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const permissionSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormUserPermission>>({
    userId: yup.string().required(t("common.permission.validation.userId.required")),
    companyId: yup.number().required(t("common.permission.validation.companyId.required")),
    permission: yup.object().required(t("common.permission.validation.permission.required")),
  });
