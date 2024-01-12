import { Td } from "components/Table";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { useMediaQuery } from "usehooks-ts";

const TerminalTableAmount = ({ minQuantity, maxQuantity }: Pick<BlockTerminalItem, "minQuantity" | "maxQuantity">) => {
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <>
      <Td data-label="Min amount" textAlign={adjustAmountAlignment ? "center" : "left"}>
        {minQuantity}
      </Td>
      <Td data-label="Max amount" textAlign={adjustAmountAlignment ? "center" : "left"}>
        {maxQuantity ?? "Infinite"}
      </Td>
    </>
  );
};

export default TerminalTableAmount;
