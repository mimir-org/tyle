import { Tooltip } from "../../../complib/data-display";
import { TokenCheckbox, TokenCheckboxProps } from "../../../complib/general";
import { SelectItem } from "../../types/SelectItem";
import { SelectItemDescription } from "./SelectItemDescription";

type SelectItemInfoCheckboxProps = TokenCheckboxProps & Omit<SelectItem, "id">;

/**
 * Component which shows a single item for a given entity in addition to its description in a popover.
 * The focusable component has checkbox semantics and can be used contexts where this is needed (e.g. multiselect, select, etc.)
 *
 * @param name of item
 * @param traits various qualities/traits that the item has
 * @param delegated
 * @constructor
 */
export const SelectItemInfoCheckbox = ({ name, traits, ...delegated }: SelectItemInfoCheckboxProps) => (
  <Tooltip content={<SelectItemDescription name={name} traits={traits} />}>
    <TokenCheckbox variant={"secondary"} {...delegated}>
      {name}
    </TokenCheckbox>
  </Tooltip>
);
