import { CheckboxProps } from "@radix-ui/react-checkbox";
import { forwardRef, ReactNode } from "react";
import { Text } from "../../../../../../complib/text";
import { AttributeCheckboxContainer } from "./AttributeCheckbox.styled";

export type AttributeCheckboxProps = CheckboxProps & {
  color: string;
  children?: ReactNode;
};

/**
 * Checkbox component which represents a single attribute for a given entity.
 * The component is used in contexts where the user selects one or more attributes for a given task.
 *
 * For documentation about the underlying checkbox component see the link below.
 * @see https://www.radix-ui.com/docs/primitives/components/checkbox
 */
export const AttributeCheckbox = forwardRef<HTMLButtonElement, AttributeCheckboxProps>(
  ({ children, color, ...delegated }, ref) => {
    return (
      <AttributeCheckboxContainer ref={ref} color={color} {...delegated}>
        <Text as={"span"} variant={"label-large"} useEllipsis ellipsisMaxLines={2}>
          {children}
        </Text>
      </AttributeCheckboxContainer>
    );
  }
);

AttributeCheckbox.displayName = "AttributeCheckbox";
