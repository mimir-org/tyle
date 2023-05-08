import { UserItem } from "common/types/userItem";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import AttributePreview from "../../../../entities/attributes/AttributePreview";
import { toFormAttributeLib } from "../../../../entities/attributes/types/formAttributeLib";
import { SearchItemActions } from "../SearchItemActions";
import { AttributeItem } from "../../../../../common/types/attributeItem";

export type AttributeSearchItemProps = AttributeItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user?: UserItem;
};

/**
 * Component which visualizes a attribute search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param attribute
 * @param user
 * @constructor
 */
export const AttributeSearchItem = ({ isSelected, setSelected, user, ...attribute }: AttributeSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<AttributePreview {...toFormAttributeLib(attribute)} />}
    description={<ItemDescription onClick={setSelected} {...attribute} />}
    actions={<SearchItemActions user={user} item={attribute} />}
  />
);
