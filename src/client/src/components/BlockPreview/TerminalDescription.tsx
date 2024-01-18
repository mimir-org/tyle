import Flexbox from "components/Flexbox";
import TerminalButton from "components/TerminalButton";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";

const TerminalDescription = ({ name, minQuantity, maxQuantity, color, direction }: Omit<BlockTerminalItem, "id">) => {
  const theme = useTheme();

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
      <TerminalButton as={"div"} color={color} direction={direction} />
      <Text variant={"body-small"}>{`${name}`}</Text>
      <Text spacing={{ ml: "auto" }} variant={"body-small"}>{`(min: ${minQuantity}${
        maxQuantity ? `, max: ${maxQuantity}` : ""
      })`}</Text>
    </Flexbox>
  );
};

export default TerminalDescription;
