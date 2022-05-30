import { Tooltip } from "../../../../../../complib/data-display";
import { AttributeItem } from "../../../../types/AttributeItem";
import { AttributeButton, AttributeButtonProps } from "./AttributeButton";
import { AttributeDescription } from "./AttributeDescription";

type AttributeInfoButtonProps = AttributeButtonProps & Omit<AttributeItem, "id">;

/**
 * Component which shows a single attribute for a given entity in addition to its description in a popover.
 * The focusable component is a button.
 *
 * @param name of attribute
 * @param color representing attribute
 * @param traits various qualities/traits that the attribute has
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const AttributeInfoButton = ({ name, color, traits, ...delegated }: AttributeInfoButtonProps) => (
  <Tooltip content={<AttributeDescription name={name} color={color} traits={traits} />}>
    <AttributeButton color={color} {...delegated}>
      {name}
    </AttributeButton>
  </Tooltip>
);
