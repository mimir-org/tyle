import { TokenCheckbox, TokenCheckboxProps, Tooltip } from "@mimirorg/component-library";
import InfoItemDescription from "components/InfoItemDescription";
import { InfoItem } from "types/infoItem";

type InfoItemCheckboxProps = TokenCheckboxProps & Omit<InfoItem, "id">;

/**
 * Component which shows a single generic item for a given entity in addition to its description(s) in a popover.
 * The focusable component has checkbox semantics and can be used contexts where this is needed (e.g. multiselect, select, etc.)
 *
 * @param name of item
 * @param descriptors various qualities/traits that the item has
 * @param delegated
 * @constructor
 */
const InfoItemCheckbox = ({ name, descriptors, ...delegated }: InfoItemCheckboxProps) => (
  <Tooltip content={<InfoItemDescription name={name} descriptors={descriptors} />}>
    <TokenCheckbox {...delegated}>{name}</TokenCheckbox>
  </Tooltip>
);

export default InfoItemCheckbox;
