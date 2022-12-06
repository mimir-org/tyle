import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { FormApproval } from "features/settings/common/approval-card/card-form/types/formApproval";

export const approvalSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormApproval>>({
    id: yup.string().required(t("common.permission.validation.userId.required")),
    objectType: yup.string().required(t("common.permission.validation.userId.required")),
    state: yup.object().required(t("common.permission.validation.userId.required")),
    companyId: yup.number().required(t("common.permission.validation.userId.required")),    
  });
