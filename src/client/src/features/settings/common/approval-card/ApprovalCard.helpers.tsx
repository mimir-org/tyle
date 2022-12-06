import { useTranslation } from "react-i18next";

export const useApprovalDescriptors = (): { [key: string]: string } => {
  const { t } = useTranslation("settings");
  const descriptors: { [key: string]: string } = {};

  descriptors[t("common.permission.email")] = "Dette er en test";

//   if (user.company) {
//     descriptors[t("common.permission.organization")] = user.company.name;
//   }
//   if (user.purpose) {
//     descriptors[t("common.permission.purpose")] = user.purpose;
//   }

  return descriptors;
};