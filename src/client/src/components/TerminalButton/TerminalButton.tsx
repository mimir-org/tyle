import { ArrowSmallRight, ArrowsRightLeft } from "@styled-icons/heroicons-outline";
import { ButtonHTMLAttributes, ElementType, ReactNode, forwardRef } from "react";
import { Polymorphic } from "types/styleProps";
import { Direction } from "types/terminals/direction";
import EllipseIcon from "./EllipseIcon";
import { TerminalButtonContainer } from "./TerminalButton.styled";

export type TerminalButtonVariant = "small" | "medium" | "large";

export type TerminalButtonProps = ButtonHTMLAttributes<HTMLButtonElement> &
  Polymorphic<ElementType> & {
    children?: ReactNode;
    direction?: Direction;
    color: string;
    variant?: TerminalButtonVariant;
  };

/**
 * Component which represents a single terminal for a given block.
 *
 * @param as polymorphic parameter for changing base element (defaults to <button>)
 * @param variant decides which button icon is used
 */
const TerminalButton = forwardRef<HTMLButtonElement, TerminalButtonProps>(
  ({ children, direction, ...delegated }, ref) => (
    <TerminalButtonContainer ref={ref} {...delegated} direction={direction}>
      {children}
      {(direction === Direction.Input || direction === Direction.Output) && <ArrowSmallRight />}
      {direction === Direction.Bidirectional && <ArrowsRightLeft />}
      {direction === undefined && <EllipseIcon color={"#FFF"} />}
    </TerminalButtonContainer>
  ),
);

TerminalButton.displayName = "TerminalButton";
TerminalButton.defaultProps = {
  type: "button",
  variant: "medium",
};

export default TerminalButton;
