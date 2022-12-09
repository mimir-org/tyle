import { UserItem } from "common/types/userItem";
import { SearchResult } from "features/explore/search/types/searchResult";

export interface ConditionalSearchItem {
  user: UserItem;
  item: SearchResult;
  isSelected?: boolean;
  setSelected?: () => void;
}
