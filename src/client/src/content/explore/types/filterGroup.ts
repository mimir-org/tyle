import { Filter } from "./filter";

export interface FilterGroup {
  name: string;
  filters?: Filter[];
}
