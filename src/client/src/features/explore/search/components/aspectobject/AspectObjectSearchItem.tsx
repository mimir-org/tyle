import { AspectObjectItem } from "common/types/aspectObjectItem";
import { UserItem } from "common/types/userItem";
import { AspectObjectPreview } from "features/common/aspectobject";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { SearchItemActions } from "../SearchItemActions";

export type AspectObjectSearchItemProps = AspectObjectItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user?: UserItem;
};

/**
 * Component which visualizes a single aspect object search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param aspectObject
 * @param user
 * @constructor
 */
export const AspectObjectSearchItem = ({
  isSelected,
  setSelected,
  user,
  ...aspectObject
}: AspectObjectSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<AspectObjectPreview {...aspectObject} />}
    description={<ItemDescription onClick={setSelected} {...aspectObject} />}
    actions={<SearchItemActions user={user} item={aspectObject} />}
  />
);
