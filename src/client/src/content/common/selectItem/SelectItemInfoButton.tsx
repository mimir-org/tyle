import { ForwardedRef, forwardRef, useState } from "react";
import { Popover } from "../../../complib/data-display";
import { TokenButton, TokenButtonProps } from "../../../complib/general";
import { SelectItemDescription, SelectItemDescriptionProps } from "./SelectItemDescription";

export type SelectItemInfoButtonProps = TokenButtonProps & SelectItemDescriptionProps;

/**
 * Component which shows a single item for a given entity in addition to its description in a popover.
 * The focusable component is a button.
 *
 * @param name of item
 * @param traits various qualities/traits that the item has
 * @param actionable enables action button in popover
 * @param actionIcon icon disabled inside action button
 * @param actionText action button text (hidden if icon is supplied)
 * @param onAction called when clicking action button
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const SelectItemInfoButton = forwardRef(
  (props: SelectItemInfoButtonProps, ref: ForwardedRef<HTMLButtonElement>) => {
    const { name, traits, actionable, actionIcon, actionText, onAction } = props;
    const [isSelected, setIsSelected] = useState(false);

    return (
      <Popover
        align={"start"}
        onOpenChange={() => setIsSelected(!isSelected)}
        content={
          <SelectItemDescription
            name={name}
            traits={traits}
            actionable={actionable}
            actionIcon={actionIcon}
            actionText={actionText}
            onAction={onAction}
          />
        }
      >
        <TokenButton ref={ref} variant={"secondary"} $selected={isSelected}>
          {name}
        </TokenButton>
      </Popover>
    );
  }
);

SelectItemInfoButton.displayName = "AttributeInfoButton";
