import { Tooltip } from "../../../complib/data-display";
import { TokenCheckbox, TokenCheckboxProps } from "../../../complib/general";
import { AttributeItem } from "../../types/AttributeItem";
import { AttributeDescription } from "./AttributeDescription";

type AttributeSingleCheckboxProps = TokenCheckboxProps & Omit<AttributeItem, "id">;

/**
 * Component which shows a single attribute for a given entity in addition to its description in a popover.
 * The focusable component has checkbox semantics and can be used contexts where this is needed (e.g. multiselect, select, etc.)
 *
 * @param name of attribute
 * @param traits various qualities/traits that the attribute has
 * @param delegated receives all properties which AttributeButtonProps define
 * @constructor
 */
export const AttributeInfoCheckbox = ({ name, traits, ...delegated }: AttributeSingleCheckboxProps) => (
  <Tooltip content={<AttributeDescription name={name} traits={traits} />}>
    <TokenCheckbox variant={"secondary"} {...delegated}>
      {name}
    </TokenCheckbox>
  </Tooltip>
);
