import { Logo } from "features/common/logo";
import { PlainLink } from "features/common/plain-link";
import { useTranslation } from "react-i18next";

export const HeaderHomeLink = () => {
  const { t } = useTranslation("ui");

  return (
    <PlainLink to={"/"} height={"100%"}>
      <Logo height={"100%"} width={"fit-content"} alt={t("header.home")} />
    </PlainLink>
  );
};
