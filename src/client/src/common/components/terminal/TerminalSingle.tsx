import { TerminalButton, TerminalButtonProps } from "common/components/terminal/TerminalButton";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Tooltip } from "complib/data-display";
import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { useTheme } from "styled-components";

/**
 * Component which shows a single terminal for a given node in addition to its name and amount in a tooltip.
 *
 * @param name
 * @param amount
 * @param color
 * @param direction
 * @param variant
 * @constructor
 */
export const TerminalSingle = ({
  name,
  maxQuantity,
  color,
  direction,
  variant,
}: NodeTerminalItem & Pick<TerminalButtonProps, "variant">) => {
  return (
    <Tooltip
      content={<TerminalDescription name={name} maxQuantity={maxQuantity} color={color} direction={direction} />}
    >
      <TerminalButton color={color} direction={direction} variant={variant} />
    </Tooltip>
  );
};

export const TerminalDescription = ({ name, maxQuantity, color, direction }: NodeTerminalItem) => {
  const theme = useTheme();

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
      <TerminalButton as={"div"} color={color} direction={direction} />
      <Text variant={"body-small"}>{`${name}`}</Text>
      <Text ml={"auto"} variant={"body-small"}>{`x${maxQuantity}`}</Text>
    </Flexbox>
  );
};
