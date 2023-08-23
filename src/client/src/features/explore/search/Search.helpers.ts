import { Link } from "common/types/link";
import { blockFormBasePath } from "features/entities/block/BlockFormRoutes";
import { terminalFormBasePath } from "features/entities/terminal/TerminalFormRoutes";
import { useTranslation } from "react-i18next";
import { attributeFormBasePath } from "../../entities/attributes/AttributeFormRoutes";
import { unitFormBasePath } from "../../entities/units/UnitFormRoutes";
import { datumFormBasePath } from "../../entities/quantityDatum/QuantityDatumFormRoutes";
import { rdsFormBasePath } from "../../entities/RDS/RdsFormRoutes";

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
      name: t("search.create.unit"),
      path: unitFormBasePath,
    },
    {
      name: t("search.create.quantityDatum"),
      path: datumFormBasePath,
    },
    {
      name: t("search.create.rds"),
      path: rdsFormBasePath,
    },
  ];
};

export const isPositiveInt = (value: string | null): boolean => {
  return value ? /^\d+$/.test(value) && Number(value) > 0 : false;
};
