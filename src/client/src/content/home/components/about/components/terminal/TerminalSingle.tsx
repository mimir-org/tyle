import { useTheme } from "styled-components";
import { Tooltip } from "../../../../../../complib/data-display";
import { Flexbox } from "../../../../../../complib/layouts";
import { Text } from "../../../../../../complib/text";
import { TerminalItem } from "../../../../types/TerminalItem";
import { TerminalButton } from "./TerminalButton";

/**
 * Component which shows a single terminal for a given node in addition to its name and amount in a tooltip.
 *
 * @param name
 * @param amount
 * @param color
 * @param direction
 * @constructor
 */
export const TerminalSingle = ({ name, amount, color, direction }: TerminalItem) => {
  return (
    <Tooltip content={<TerminalDescription name={name} amount={amount} color={color} direction={direction} />}>
      <TerminalButton color={color} variant={direction} />
    </Tooltip>
  );
};

export const TerminalDescription = ({ name, amount, color, direction }: TerminalItem) => {
  const theme = useTheme();

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
      <TerminalButton as={"div"} color={color} variant={direction} />
      <Text variant={"body-medium"}>{`${name}`}</Text>
      <Text ml={"auto"} variant={"body-medium"}>{`x${amount}`}</Text>
    </Flexbox>
  );
};
