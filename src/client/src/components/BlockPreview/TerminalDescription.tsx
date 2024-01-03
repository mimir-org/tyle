import Flexbox from "components/Flexbox";
import TerminalButton from "components/TerminalButton";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "./blockTerminalQuantityRestrictions";

const TerminalDescription = ({ name, maxQuantity, color, direction }: Omit<BlockTerminalItem, "id">) => {
  const theme = useTheme();
  const shownQuantity = maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? "Infinite" : maxQuantity;

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
      <TerminalButton as={"div"} color={color} direction={direction} />
      <Text variant={"body-small"}>{`${name}`}</Text>
      <Text spacing={{ ml: "auto" }} variant={"body-small"}>{`x ${shownQuantity}`}</Text>
    </Flexbox>
  );
};

export default TerminalDescription;
