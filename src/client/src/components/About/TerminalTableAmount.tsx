import { Td } from "@mimirorg/component-library";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "components/BlockPreview/blockTerminalQuantityRestrictions";
import { useTranslation } from "react-i18next";
import { useMediaQuery } from "usehooks-ts";

const TerminalTableAmount = ({ maxQuantity }: Pick<BlockTerminalItem, "maxQuantity">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const { t } = useTranslation("explore");

  return (
    <Td data-label={t("about.terminals.amount")} textAlign={adjustAmountAlignment ? "center" : "left"}>
      {maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? t("about.terminals.infinite") : maxQuantity}
    </Td>
  );
};

export default TerminalTableAmount;
