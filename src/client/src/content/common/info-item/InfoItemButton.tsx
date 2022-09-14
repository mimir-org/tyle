import { ForwardedRef, forwardRef, useState } from "react";
import { Popover } from "../../../complib/data-display";
import { TokenButton, TokenButtonProps } from "../../../complib/general";
import { InfoItemDescription, InfoItemDescriptionProps } from "./InfoItemDescription";

export type InfoItemButtonProps = TokenButtonProps & InfoItemDescriptionProps;

/**
 * Component which shows a single generic item for a given entity in addition to its description(s) in a popover.
 * The focusable component is a button.
 *
 * @param name of item
 * @param descriptors various qualities/traits that the item has
 * @param actionable enables action button in popover
 * @param actionIcon icon disabled inside action button
 * @param actionText action button text (hidden if icon is supplied)
 * @param onAction called when clicking action button
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const InfoItemButton = forwardRef((props: InfoItemButtonProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const { name, descriptors, actionable, actionIcon, actionText, onAction } = props;
  const [isSelected, setIsSelected] = useState(false);

  return (
    <Popover
      align={"start"}
      onOpenChange={() => setIsSelected(!isSelected)}
      content={
        <InfoItemDescription
          name={name}
          descriptors={descriptors}
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
});

InfoItemButton.displayName = "AttributeInfoButton";
