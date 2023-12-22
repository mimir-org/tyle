import { Flexbox, Td } from "@mimirorg/component-library";
import TerminalButton from "components/TerminalButton";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";

const TerminalTableIdentifier = ({
  name,
  color,
  direction,
}: Pick<BlockTerminalItem, "name" | "color" | "direction">) => {
  const theme = useTheme();

  return (
    <Td data-label="Terminal name">
      <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
        <TerminalButton variant={"small"} as={"div"} color={color} direction={direction} />
        {name}
      </Flexbox>
    </Td>
  );
};

export default TerminalTableIdentifier;
