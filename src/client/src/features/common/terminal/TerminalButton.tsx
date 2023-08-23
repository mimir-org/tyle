import { ArrowSmallRight, ArrowsRightLeft } from "@styled-icons/heroicons-outline";
import { BlockTerminalItemDirection } from "common/types/blockTerminalItem";
import { Polymorphic } from "@mimirorg/component-library";
import { EllipseIcon } from "features/common/terminal/assets";
import { TerminalButtonContainer } from "features/common/terminal/TerminalButton.styled";
import { ButtonHTMLAttributes, ElementType, forwardRef, ReactNode } from "react";

export type TerminalButtonVariant = "small" | "medium" | "large";

export type TerminalButtonProps = ButtonHTMLAttributes<HTMLButtonElement> &
  Polymorphic<ElementType> & {
    children?: ReactNode;
    direction?: BlockTerminalItemDirection;
    color: string;
    variant?: TerminalButtonVariant;
  };

/**
 * Component which represents a single terminal for a given block.
 *
 * @param as polymorphic parameter for changing base element (defaults to <button>)
 * @param variant decides which button icon is used
 */
export const TerminalButton = forwardRef<HTMLButtonElement, TerminalButtonProps>(
  ({ children, direction, ...delegated }, ref) => (
    <TerminalButtonContainer ref={ref} {...delegated} direction={direction}>
      {children}
      {(direction === "Input" || direction === "Output") && <ArrowSmallRight />}
      {direction === "Bidirectional" && <ArrowsRightLeft />}
      {!direction && <EllipseIcon color={"#FFF"} />}
    </TerminalButtonContainer>
  ),
);

TerminalButton.displayName = "TerminalButton";
TerminalButton.defaultProps = {
  type: "button",
  variant: "medium",
};
