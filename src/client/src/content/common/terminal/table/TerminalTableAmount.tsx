import textResources from "../../../../assets/text/TextResources";
import { Td } from "../../../../complib/data-display";
import { useMediaQuery } from "../../../../hooks/useMediaQuery";
import { TerminalItem } from "../../../types/TerminalItem";

export const TerminalTableAmount = ({ amount }: Pick<TerminalItem, "amount">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <Td data-label={textResources.TERMINAL_TABLE_AMOUNT} textAlign={adjustAmountAlignment ? "center" : "left"}>
      {amount}
    </Td>
  );
};