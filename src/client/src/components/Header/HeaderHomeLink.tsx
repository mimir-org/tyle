import { Logo } from "components/Logo";
import { PlainLink } from "components/PlainLink";
import { useTranslation } from "react-i18next";

export const HeaderHomeLink = () => {
  const { t } = useTranslation("ui");

  return (
    <PlainLink to={"/"} height={"100%"}>
      <Logo height={"100%"} width={"100%"} alt={t("header.home")} />
    </PlainLink>
  );
};
