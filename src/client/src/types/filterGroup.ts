import { Filter } from "types/filter";

export interface FilterGroup {
  name: string;
  filters?: Filter[];
}
