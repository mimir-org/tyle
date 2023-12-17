import { Flexbox, Text } from "@mimirorg/component-library";
import TerminalButton from "components/TerminalButton";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "./blockTerminalQuantityRestrictions";

const TerminalDescription = ({ name, maxQuantity, color, direction }: Omit<BlockTerminalItem, "id">) => {
  const theme = useTheme();
  const shownQuantity = maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? "Infinite" : maxQuantity;

  return (
    <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
      <TerminalButton as={"div"} color={color} direction={direction} />
      <Text variant={"body-small"}>{`${name}`}</Text>
      <Text spacing={{ ml: "auto" }} variant={"body-small"}>{`x ${shownQuantity}`}</Text>
    </Flexbox>
  );
};

export default TerminalDescription;
