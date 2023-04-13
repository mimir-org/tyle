import { Link } from "common/types/link";
import { aspectObjectFormBasePath } from "features/entities/aspectobject/AspectObjectFormRoutes";
import { terminalFormBasePath } from "features/entities/terminal/TerminalFormRoutes";
import { useTranslation } from "react-i18next";

export const useCreateMenuLinks = (): Link[] => {
  const { t } = useTranslation("explore");

  return [
    {
      name: t("search.create.aspectObject"),
      path: aspectObjectFormBasePath,
    },
    {
      name: t("search.create.terminal"),
      path: terminalFormBasePath,
    },
  ];
};
