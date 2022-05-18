import { AttributeButtonContainer, AttributeButtonContainerProps } from "./AttributeButton.styled";
import { ElementType, forwardRef } from "react";
import { Polymorphic, TextTypes } from "../../../../../../complib/props";
import { Text } from "../../../../../../complib/text";

type AttributeButtonProps = AttributeButtonContainerProps &
  Polymorphic<ElementType> & {
    children?: string;
    variant?: "large" | "medium";
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
