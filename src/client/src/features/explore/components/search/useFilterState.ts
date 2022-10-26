import { useState } from "react";
import { Filter } from "../../types/filter";

export const useFilterState = (initial: Filter[]): [filters: Filter[], toggleFilter: (filter: Filter) => void] => {
  const [filters, setFilters] = useState<Filter[]>(initial);

  const toggleFilter = (filter: Filter) => {
    const filterIsActive = filters.some((x) => x.value === filter.value);

    if (filterIsActive) {
      setFilters(filters.filter((f) => f.value !== filter.value));
    } else {
      setFilters([...filters, filter]);
    }
  };

  return [filters, toggleFilter];
};