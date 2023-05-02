import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "common/utils/aspectObjectTerminalQuantityRestrictions";
import { Td } from "complib/data-display";
import { useTranslation } from "react-i18next";
import { useMediaQuery } from "usehooks-ts";

export const TerminalTableAmount = ({ maxQuantity }: Pick<AspectObjectTerminalItem, "maxQuantity">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const { t } = useTranslation("explore");

  return (
    <Td data-label={t("about.terminals.amount")} textAlign={adjustAmountAlignment ? "center" : "left"}>
      {maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? t("about.terminals.infinite") : maxQuantity}
    </Td>
  );
};
