import { Tooltip } from "@mimirorg/component-library";
import TerminalButton, { TerminalButtonProps } from "components/TerminalButton";
import { BlockTerminalItem } from "types/blockTerminalItem";
import TerminalDescription from "./TerminalDescription";

/**
 * Component which shows a single terminal for a given block in addition to its name and amount in a tooltip.
 *
 * @param name
 * @param amount
 * @param color
 * @param direction
 * @param variant
 * @constructor
 */
const TerminalSingle = ({
  name,
  maxQuantity,
  color,
  direction,
  variant,
}: BlockTerminalItem & Pick<TerminalButtonProps, "variant">) => {
  return (
    <Tooltip
      content={<TerminalDescription name={name} maxQuantity={maxQuantity} color={color} direction={direction} />}
    >
      <TerminalButton color={color} direction={direction} variant={variant} />
    </Tooltip>
  );
};

export default TerminalSingle;
