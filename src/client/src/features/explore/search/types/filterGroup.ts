import { Filter } from "features/explore/search/types/filter";

export interface FilterGroup {
  name: string;
  filters?: Filter[];
}
