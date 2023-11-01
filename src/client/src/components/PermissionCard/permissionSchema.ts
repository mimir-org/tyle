import { TFunction } from "i18next";
import { YupShape } from "types/yupShape";
import * as yup from "yup";
import { FormUserPermission } from "./formUserPermission";

export const permissionSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormUserPermission>>({
    userId: yup.string().required(t("common.permission.validation.userId.required")),
    companyId: yup.number().required(t("common.permission.validation.companyId.required")),
    permission: yup.object().required(t("common.permission.validation.permission.required")),
  });
