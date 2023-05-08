import { TerminalItem } from "common/types/terminalItem";
import { UserItem } from "common/types/userItem";
import { TerminalPreview } from "features/common/terminal/TerminalPreview";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { SearchItemActions } from "../SearchItemActions";

export type TerminalSearchItemProps = TerminalItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user?: UserItem;
};

/**
 * Component which visualizes a terminal search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param terminal
 * @param user
 * @constructor
 */
export const TerminalSearchItem = ({ isSelected, setSelected, user, ...terminal }: TerminalSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<TerminalPreview {...terminal} />}
    description={<ItemDescription onClick={setSelected} {...terminal} />}
    actions={<SearchItemActions user={user} item={terminal} />}
  />
);
