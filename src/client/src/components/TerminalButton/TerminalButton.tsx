import { Polymorphic } from "@mimirorg/component-library";
import { ArrowSmallRight, ArrowsRightLeft } from "@styled-icons/heroicons-outline";
import { Direction } from "common/types/terminals/direction";
import { EllipseIcon } from "components/TerminalButton/EllipseIcon";
import { TerminalButtonContainer } from "components/TerminalButton/TerminalButton.styled";
import { ButtonHTMLAttributes, ElementType, ReactNode, forwardRef } from "react";

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
      {!direction && <EllipseIcon color={"#FFF"} />}
    </TerminalButtonContainer>
  ),
);

TerminalButton.displayName = "TerminalButton";
TerminalButton.defaultProps = {
  type: "button",
  variant: "medium",
};

export default TerminalButton;
