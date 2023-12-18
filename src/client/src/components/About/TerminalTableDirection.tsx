import { Flexbox, Td } from "@mimirorg/component-library";
import { ArrowLeft, ArrowRight, ArrowsRightLeft } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { Direction } from "types/terminals/direction";

const TerminalTableDirection = ({ direction }: Pick<BlockTerminalItem, "direction">) => {
  const theme = useTheme();
  const directionIconSize = 20;

  return (
    <Td data-label="Terminal direction">
      <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
        {direction === Direction.Input && (
          <ArrowRight color={theme.mimirorg.color.primary.base} size={directionIconSize} />
        )}
        {direction === Direction.Output && (
          <ArrowLeft color={theme.mimirorg.color.primary.base} size={directionIconSize} />
        )}
        {direction === Direction.Bidirectional && (
          <ArrowsRightLeft color={theme.mimirorg.color.primary.base} size={directionIconSize} />
        )}
        {direction}
      </Flexbox>
    </Td>
  );
};

export default TerminalTableDirection;
