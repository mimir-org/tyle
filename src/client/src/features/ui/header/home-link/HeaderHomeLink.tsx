import { Logo } from "common/components/logo";
import { PlainLink } from "common/components/plain-link";
import { useTranslation } from "react-i18next";

export const HeaderHomeLink = () => {
  const { t } = useTranslation();

  return (
    <PlainLink to={"/"} height={"100%"}>
      <Logo height={"100%"} width={"fit-content"} alt={t("header.home")} />
    </PlainLink>
  );
};
