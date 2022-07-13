import { Aspect, NodeLibCm } from "@mimirorg/typelibrary-types";
import { useState } from "react";
import { useGetNodes } from "../../../../data/queries/tyle/queriesNode";
import { useGetPurposes } from "../../../../data/queries/tyle/queriesPurpose";
import { useFuse } from "../../../../hooks/useFuse";
import { getValueLabelObjectsFromEnum } from "../../../../utils/getValueLabelObjectsFromEnum";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";
import { Filter } from "../../types/Filter";
import { FilterGroup } from "../../types/FilterGroup";
import { NodeItem } from "../../types/NodeItem";

/**
 * Indexed fields that the fuzzy-search will try to match a query against
 */
export const searchKeys = [
  "id",
  "name",
  "description",
  "aspect",
  "rdsName",
  "purposeName",
  "nodeTerminals.terminal.name",
];

export const useFilterState = (
  initialFilters: Filter[]
): [filters: Filter[], toggleFilter: (filter: Filter) => void] => {
  const [filters, setFilters] = useState<Filter[]>(initialFilters);

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

export const useSearchResults = (query: string, filters: Filter[]): [results: NodeItem[], isLoading: boolean] => {
  const nodeQuery = useGetNodes();
  const useFilters = filters.length > 0;
  const searchableData = nodeQuery.data ?? [];

  const fuseResult = useFuse<NodeLibCm>(searchableData, query, { keys: searchKeys, matchAllOnEmpty: true });

  const processedResults = useFilters
    ? fuseResult.filter((x) => filters.some((f) => x.item[f.key as keyof NodeLibCm] == f.value))
    : fuseResult;

  const mappedResults = processedResults.map((x) => mapNodeLibCmToNodeItem(x.item));

  return [mappedResults, nodeQuery.isLoading];
};

export const useGetFilterGroups = (): FilterGroup[] => {
  const categoryFilters: FilterGroup[] = [];
  const purposeQuery = useGetPurposes();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);

  categoryFilters.push({
    name: "Aspect",
    filters: aspectOptions.map((a) => ({
      key: "aspect",
      label: a.label,
      value: a.value.toString(),
    })),
  });

  categoryFilters.push({
    name: "Purpose",
    filters: purposeQuery.data?.map((p) => ({ key: "purposeName", label: p.name, value: p.name })),
  });

  return categoryFilters;
};
