import { ArrowTopRightOnSquare } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import UserMenuButton from "./UserMenuButton";

const FeedbackButton = () => {
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

export default FeedbackButton;
