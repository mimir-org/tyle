import { useTranslation } from "react-i18next";
import { Td } from "../../../../complib/data-display";
import { useMediaQuery } from "../../../../hooks/useMediaQuery";
import { NodeTerminalItem } from "../../../types/NodeTerminalItem";

export const TerminalTableAmount = ({ amount }: Pick<NodeTerminalItem, "amount">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });

  return (
    <Td data-label={t("amount")} textAlign={adjustAmountAlignment ? "center" : "left"}>
      {amount}
    </Td>
  );
};
