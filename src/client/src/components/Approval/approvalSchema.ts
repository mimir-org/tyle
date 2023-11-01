import { TFunction } from "i18next";
import { YupShape } from "types/yupShape";
import * as yup from "yup";
import { FormApproval } from "./formApproval";

export const approvalSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormApproval>>({
    id: yup.string().required(t("common.approval.validation.id.required")),
    objectType: yup.string().required(t("common.approval.validation.objectType.required")),
    state: yup.object().required(t("common.approval.validation.state.required")),
    companyId: yup.number().required(t("common.approval.validation.companyId.required")),
  });
