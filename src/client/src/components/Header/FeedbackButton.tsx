import { ArrowTopRightOnSquare } from "@styled-icons/heroicons-outline";
import { UserMenuButton } from "components/Header/UserMenuButton";
import { useTranslation } from "react-i18next";

export const FeedbackButton = () => {
  const { t } = useTranslation("ui");

  return (
    <UserMenuButton
      icon={<ArrowTopRightOnSquare size={24} />}
      onClick={() =>
        window.open("https://github.com/mimir-org/typelibrary/issues/new/choose", "_blank", "rel=noopener noreferrer")
      }
    >
      {t("header.menu.feedback")}
    </UserMenuButton>
  );
};
