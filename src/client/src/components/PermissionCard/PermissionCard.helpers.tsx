import { useTranslation } from "react-i18next";
import { UserItem } from "types/userItem";

export const useUserDescriptors = (user: UserItem): { [key: string]: string } => {
  const { t } = useTranslation("settings");
  const descriptors: { [key: string]: string } = {};

  descriptors[t("common.permission.email")] = user.email;

  if (user.purpose) {
    descriptors[t("common.permission.purpose")] = user.purpose;
  }

  return descriptors;
};
