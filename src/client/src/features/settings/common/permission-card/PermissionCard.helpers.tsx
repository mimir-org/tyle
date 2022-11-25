import { UserItem } from "common/types/userItem";
import { useTranslation } from "react-i18next";

export const useUserDescriptors = (user: UserItem): { [key: string]: string } => {
  const { t } = useTranslation();
  const descriptors: { [key: string]: string } = {};

  descriptors[t("settings.access.email")] = user.email;

  if (user.company) {
    descriptors[t("settings.access.organization")] = user.company.name;
  }
  if (user.purpose) {
    descriptors[t("settings.access.purpose")] = user.purpose;
  }

  return descriptors;
};
