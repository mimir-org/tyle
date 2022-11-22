import { MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { useTranslation } from "react-i18next";

export const useUserDescriptors = (user: MimirorgUserCm): { [key: string]: string } => {
  const { t } = useTranslation();
  const descriptors: { [key: string]: string } = {};

  descriptors[t("settings.access.email")] = user.email;

  if (user.companyName) {
    descriptors[t("settings.access.organization")] = user.companyName;
  }
  if (user.purpose) {
    descriptors[t("settings.access.purpose")] = user.purpose;
  }

  return descriptors;
};
