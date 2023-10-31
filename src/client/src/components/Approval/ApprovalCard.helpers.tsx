import { useTranslation } from "react-i18next";
import { ApprovalCm, State } from "@mimirorg/typelibrary-types";

export const useApprovalDescriptors = (approval: ApprovalCm): { [key: string]: string } => {
  const { t } = useTranslation("settings");
  const descriptors: { [key: string]: string } = {};

  if (approval.companyName) {
    descriptors[t("common.approval.company")] = approval.companyName;
  }

  if (approval.objectType) {
    descriptors[t("common.approval.objectType")] = approval.objectType;
  }

  if (approval.userName) {
    descriptors[t("common.approval.userName")] = approval.userName;
  }

  if (approval.stateName) {
    descriptors[t("common.approval.stateName")] = approval.stateName;
  }

  return descriptors;
};

export const approvalFilter = (approval: ApprovalCm): boolean => {
  if (approval == null) return false;

  return approval.state === State.Review;
};
