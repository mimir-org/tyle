import { attributeFormBasePath } from "components/AttributeForm/AttributeFormRoutes";
import { attributeGroupFormBasePath } from "components/AttributeGroupForm/AttributeGroupFormRoutes";
import { blockFormBasePath } from "components/BlockForm/BlockFormRoutes";
import { terminalFormBasePath } from "components/TerminalForm/TerminalFormRoutes";
import { useTranslation } from "react-i18next";
import { Link } from "types/link";

export const useCreateMenuLinks = (): Link[] => {
  const { t } = useTranslation("explore");

  return [
    {
      name: t("search.create.block"),
      path: blockFormBasePath,
    },
    {
      name: t("search.create.terminal"),
      path: terminalFormBasePath,
    },
    {
      name: t("search.create.attribute"),
      path: attributeFormBasePath,
    },
    {
      name: t("search.create.attributeGroup"),
      path: attributeGroupFormBasePath,
    },
  ];
};

export const isPositiveInt = (value: string | null): boolean => {
  return value ? /^\d+$/.test(value) && Number(value) > 0 : false;
};
