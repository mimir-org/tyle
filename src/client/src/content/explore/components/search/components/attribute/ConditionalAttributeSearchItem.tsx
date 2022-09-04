import { isAttributeItem } from "../../../../../../utils/guards";
import { ConditionalSearchItem } from "../../../../types/conditionalSearchItem";
import { AttributeSearchItem, AttributeSearchItemProps } from "./AttributeSearchItem";

type Props = ConditionalSearchItem & Pick<AttributeSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of an attribute given an unknown search item type.
 * Performs type check on the item property and renders if it matches the attribute type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @constructor
 */
export const ConditionalAttributeSearchItem = ({ item, isSelected, setSelected }: Props) => {
  if (isAttributeItem(item)) {
    return <AttributeSearchItem key={item.id} isSelected={isSelected} setSelected={setSelected} {...item} />;
  }

  return <></>;
};
