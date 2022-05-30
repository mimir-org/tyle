import { Tooltip } from "../../../../../../complib/data-display";
import { AttributeItem } from "../../../../types/AttributeItem";
import { AttributeCheckbox, AttributeCheckboxProps } from "./AttributeCheckbox";
import { AttributeDescription } from "./AttributeDescription";

type AttributeSingleCheckboxProps = AttributeCheckboxProps & Omit<AttributeItem, "id">;

/**
 * Component which shows a single attribute for a given entity in addition to its description in a popover.
 * The focusable component has checkbox semantics and can be used contexts where this is needed (e.g. multiselect, select, etc.)
 *
 * @param name of attribute
 * @param color representing attribute
 * @param traits various qualities/traits that the attribute has
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const AttributeInfoCheckbox = ({ name, color, traits, ...delegated }: AttributeSingleCheckboxProps) => (
  <Tooltip content={<AttributeDescription name={name} color={color} traits={traits} />}>
    <AttributeCheckbox color={color} {...delegated}>
      {name}
    </AttributeCheckbox>
  </Tooltip>
);
