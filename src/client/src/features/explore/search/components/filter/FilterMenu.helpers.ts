import { Filter } from "../../types/filter";

export const filterAvailableFilters = (query: string, filters?: Filter[]) =>
  query ? filters?.filter((x) => x.label.toLowerCase().includes(query.toLowerCase())) : filters;
