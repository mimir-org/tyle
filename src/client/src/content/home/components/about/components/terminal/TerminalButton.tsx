import { ButtonHTMLAttributes, ElementType, forwardRef, ReactNode } from "react";
import { Polymorphic } from "../../../../../../complib/props";
import { TerminalButtonContainer, ThickPlus, ThickSwitchHorizontal } from "./TerminalButton.styled";

export type TerminalButtonVariant = "small" | "medium" | "large";

export type TerminalButtonProps = ButtonHTMLAttributes<HTMLButtonElement> &
  Polymorphic<ElementType> & {
    children?: ReactNode;
    direction?: "Input" | "Output" | "Bidirectional";
    color: string;
    variant?: TerminalButtonVariant;
  };

/**
 * Component which represents a single terminal for a given node.
 *
 * @param as polymorphic parameter for changing base element (defaults to <button>)
 * @param variant decides which button icon is used
 */
export const TerminalButton = forwardRef<HTMLButtonElement, TerminalButtonProps>(
  ({ children, direction, ...delegated }, ref) => (
    <TerminalButtonContainer ref={ref} {...delegated}>
      {children}
      {direction !== "Bidirectional" ? <ThickPlus size={"100%"} /> : <ThickSwitchHorizontal size={"100%"} />}
    </TerminalButtonContainer>
  )
);

TerminalButton.displayName = "TerminalButton";
TerminalButton.defaultProps = {
  type: "button",
  direction: "Input",
  variant: "medium",
};
