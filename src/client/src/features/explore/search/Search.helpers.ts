import { Link } from "common/types/link";
import { blockFormBasePath } from "features/entities/block/BlockFormRoutes";
import { terminalFormBasePath } from "features/entities/terminal/TerminalFormRoutes";
import { useTranslation } from "react-i18next";
import { attributeFormBasePath } from "../../entities/attributes/AttributeFormRoutes";
import { attributeGroupFormBasePath } from "features/entities/attributeGroups/AttributeGroupFormRoutes";

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
