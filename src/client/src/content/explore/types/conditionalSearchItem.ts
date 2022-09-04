import { SearchResult } from "./searchResult";

export interface ConditionalSearchItem {
  item: SearchResult;
  isSelected?: boolean;
  setSelected?: () => void;
}
