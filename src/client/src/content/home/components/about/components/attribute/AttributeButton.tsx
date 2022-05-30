import { ButtonHTMLAttributes, ElementType, forwardRef } from "react";
import { Polymorphic, TextTypes } from "../../../../../../complib/props";
import { Text } from "../../../../../../complib/text";
import { AttributeButtonContainer } from "./AttributeButton.styled";

export type AttributeButtonProps = ButtonHTMLAttributes<HTMLButtonElement> &
  Polymorphic<ElementType> & {
    color: string;
    variant?: "large" | "medium" | "small";
    children?: string;
  };

/**
 * Button component which represents a single attribute for a given entity.
 *
 * @param as polymorphic parameter for changing base element (defaults to <button>)
 * @param variant decides which button size is used
 */
export const AttributeButton = forwardRef<HTMLButtonElement, AttributeButtonProps>(
  ({ children, variant, ...delegated }, ref) => {
    const textVariant = `label-${variant}` as TextTypes;

    return (
      <AttributeButtonContainer ref={ref} variant={variant} {...delegated}>
        <Text as={"span"} variant={textVariant} useEllipsis ellipsisMaxLines={2}>
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
