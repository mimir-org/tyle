import { Ref, useState } from "react";
import { Popover } from "../../../complib/data-display";
import { TokenButton, TokenButtonProps } from "../../../complib/general";
import { AttributeDescription, AttributeDescriptionProps } from "./AttributeDescription";

export type AttributeInfoButtonProps = TokenButtonProps &
  AttributeDescriptionProps & {
    buttonRef?: Ref<HTMLButtonElement>;
  };

/**
 * Component which shows a single attribute for a given entity in addition to its description in a popover.
 * The focusable component is a button.
 *
 * @param name of attribute
 * @param traits various qualities/traits that the attribute has
 * @param actionable enables action button in popover
 * @param actionIcon icon disabled inside action button
 * @param actionText action button text (hidden if icon is supplied)
 * @param onAction called when clicking action button
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const AttributeInfoButton = ({
  name,
  traits,
  actionable,
  actionIcon,
  actionText,
  onAction,
  buttonRef,
}: AttributeInfoButtonProps) => {
  const [isSelected, setIsSelected] = useState(false);

  return (
    <Popover
      align={"start"}
      onOpenChange={() => setIsSelected(!isSelected)}
      content={
        <AttributeDescription
          name={name}
          traits={traits}
          actionable={actionable}
          actionIcon={actionIcon}
          actionText={actionText}
          onAction={onAction}
        />
      }
    >
      <TokenButton ref={buttonRef} variant={"secondary"} $selected={isSelected}>
        {name}
      </TokenButton>
    </Popover>
  );
};
