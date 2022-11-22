import { SearchResult } from "features/explore/search/types/searchResult";

export interface ConditionalSearchItem {
  item: SearchResult;
  isSelected?: boolean;
  setSelected?: () => void;
}
