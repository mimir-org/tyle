import { Aspect, AttributeLibCm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { useState } from "react";
import { useGetAttributes } from "../../../../data/queries/tyle/queriesAttribute";
import { useGetNodes } from "../../../../data/queries/tyle/queriesNode";
import { useGetPurposes } from "../../../../data/queries/tyle/queriesPurpose";
import { useFuse } from "../../../../hooks/useFuse";
import { getValueLabelObjectsFromEnum } from "../../../../utils/getValueLabelObjectsFromEnum";
import { isAttributeLibCm, isNodeLibCm } from "../../../../utils/guards";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";
import { mapAttributeLibCmToAttributeItem } from "../../../../utils/mappers/mapAttributeLibCmToAttributeItem";
import { Filter } from "../../../types/Filter";
import { FilterGroup } from "../../../types/FilterGroup";
import { SearchResult } from "../../types/searchResult";

/**
 * Indexed fields that the fuzzy-search will try to match a query against
 */
const searchKeys = [
  "id",
  "name",
  "description",
  "aspect",
  "rdsName",
  "purposeName",
  "nodeTerminals.terminal.name",
  "attributeQualifier",
  "attributeSource",
  "attributeCondition",
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

export const useSearchResults = (
  query: string,
  filters: Filter[],
  pageSize = 20
): [results: SearchResult[], hits: number, isLoading: boolean] => {
  const nodeQuery = useGetNodes();
  const attributeQuery = useGetAttributes();
  const useFilters = filters.length > 0;
  const results: SearchResult[] = [];

  const searchableData = [...(nodeQuery.data ?? []), ...(attributeQuery.data ?? [])];
  const fuseResult = useFuse(searchableData, query, { keys: searchKeys, matchAllOnEmpty: true });

  const filteredResults = useFilters
    ? fuseResult.filter((x) => filters.some((f) => x.item[f.key as keyof (NodeLibCm | AttributeLibCm)] == f.value))
    : fuseResult;

  filteredResults.slice(0, pageSize).forEach((x) => {
    if (isNodeLibCm(x.item)) results.push(mapNodeLibCmToNodeItem(x.item));
    else if (isAttributeLibCm(x.item)) results.push(mapAttributeLibCmToAttributeItem(x.item));
  });

  return [results, filteredResults.length, nodeQuery.isLoading];
};

export const useGetFilterGroups = (): FilterGroup[] => {
  const categoryFilters: FilterGroup[] = [];
  const purposeQuery = useGetPurposes();
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);

  categoryFilters.push({
    name: "Entity",
    filters: [
      {
        key: "kind",
        label: "Node",
        value: "NodeLibCm",
      },
      {
        key: "kind",
        label: "Attribute",
        value: "AttributeLibCm",
      },
    ],
  });

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
