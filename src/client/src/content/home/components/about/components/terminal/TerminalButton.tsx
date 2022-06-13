import { ElementType, forwardRef, ReactNode } from "react";
import { Polymorphic } from "../../../../../../complib/props";
import {
  TerminalButtonContainer,
  TerminalButtonContainerProps,
  ThickPlus,
  ThickSwitchHorizontal,
} from "./TerminalButton.styled";

type TerminalButtonProps = TerminalButtonContainerProps &
  Polymorphic<ElementType> & {
    children?: ReactNode;
    variant?: "Input" | "Output" | "Bidirectional";
  };

/**
 * Component which represents a single terminal for a given node.
 *
 * @param as polymorphic parameter for changing base element (defaults to <button>)
 * @param variant decides which button icon is used
 */
export const TerminalButton = forwardRef<HTMLButtonElement, TerminalButtonProps>(
  ({ children, variant, ...delegated }, ref) => (
    <TerminalButtonContainer ref={ref} {...delegated}>
      {children}
      {variant !== "Bidirectional" ? <ThickPlus size={"100%"} /> : <ThickSwitchHorizontal size={"100%"} />}
    </TerminalButtonContainer>
  )
);

TerminalButton.displayName = "TerminalButton";
TerminalButton.defaultProps = {
  type: "button",
  variant: "Input",
};
