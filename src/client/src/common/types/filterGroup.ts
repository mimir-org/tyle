import { Filter } from "common/types/filter";

export interface FilterGroup {
  name: string;
  filters?: Filter[];
}
