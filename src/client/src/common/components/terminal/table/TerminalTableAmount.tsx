import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "common/utils/nodeTerminalQuantityRestrictions";
import { Td } from "complib/data-display";
import { useTranslation } from "react-i18next";
import { useMediaQuery } from "usehooks-ts";

export const TerminalTableAmount = ({ maxQuantity }: Pick<NodeTerminalItem, "maxQuantity">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const { t } = useTranslation();

  return (
    <Td data-label={t("terminals.amount")} textAlign={adjustAmountAlignment ? "center" : "left"}>
      {maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? t("terminals.infinite") : maxQuantity}
    </Td>
  );
};
