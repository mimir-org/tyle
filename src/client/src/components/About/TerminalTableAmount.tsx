import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "components/BlockPreview/blockTerminalQuantityRestrictions";
import { Td } from "components/Table";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { useMediaQuery } from "usehooks-ts";

const TerminalTableAmount = ({ maxQuantity }: Pick<BlockTerminalItem, "maxQuantity">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <Td data-label="Max amount" textAlign={adjustAmountAlignment ? "center" : "left"}>
      {maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? "Infinite" : maxQuantity}
    </Td>
  );
};

export default TerminalTableAmount;
