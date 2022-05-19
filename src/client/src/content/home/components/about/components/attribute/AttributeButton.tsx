import { AttributeButtonContainer } from "./AttributeButton.styled";
import { ButtonHTMLAttributes, ElementType, forwardRef } from "react";
import { Polymorphic, TextTypes } from "../../../../../../complib/props";
import { Text } from "../../../../../../complib/text";

export type AttributeButtonProps = ButtonHTMLAttributes<HTMLButtonElement> &
  Polymorphic<ElementType> & {
    children?: string;
    color: string;
    variant?: "large" | "medium" | "small";
  };

/**
 * Component which represents a single attribute for a given entity.
 *
 * @param as polymorphic parameter for changing base element (defaults to <button>)
 * @param variant decides which button size is used
 */
export const AttributeButton = forwardRef<HTMLButtonElement, AttributeButtonProps>(
  ({ children, variant, ...delegated }, ref) => {
    const textVariant = `label-${variant}` as TextTypes;

    return (
      <AttributeButtonContainer ref={ref} variant={variant} {...delegated}>
        <Text as={"span"} useEllipsis variant={textVariant}>
          {children}
        </Text>
      </AttributeButtonContainer>
    );
  }
);

AttributeButton.displayName = "AttributeButton";
AttributeButton.defaultProps = {
  variant: "large",
};
