import { Aspect } from "@mimirorg/typelibrary-types";
import { useState } from "react";
import { useGetAttributes } from "../../../../data/queries/tyle/queriesAttribute";
import { useGetInterfaces } from "../../../../data/queries/tyle/queriesInterface";
import { useGetNodes } from "../../../../data/queries/tyle/queriesNode";
import { useGetPurposes } from "../../../../data/queries/tyle/queriesPurpose";
import { useGetTerminals } from "../../../../data/queries/tyle/queriesTerminal";
import { useGetTransports } from "../../../../data/queries/tyle/queriesTransport";
import { useFuse } from "../../../../hooks/useFuse";
import { getValueLabelObjectsFromEnum } from "../../../../utils/getValueLabelObjectsFromEnum";
import {
  isAttributeLibCm,
  isInterfaceLibCm,
  isNodeLibCm,
  isTerminalLibCm,
  isTransportLibCm,
} from "../../../../utils/guards";
import {
  mapAttributeLibCmToAttributeItem,
  mapInterfaceLibCmToInterfaceItem,
  mapNodeLibCmToNodeItem,
  mapTerminalLibCmToTerminalItem,
  mapTransportLibCmToTransportItem,
} from "../../../../utils/mappers";
import { Filter } from "../../types/filter";
import { FilterGroup } from "../../types/filterGroup";
import { Link } from "../../types/link";
import { SearchResult, SearchResultRaw } from "../../types/searchResult";

/**
 * Indexed fields that the fuzzy-search will try to match a query against
 */
const searchKeys = ["id", "name", "description", "aspect", "nodeTerminals.terminal.name", "companyName"];

export const getCreateMenuLinks = (): Link[] => {
  return [
    {
      name: "Attribute",
      path: "/form/attribute",
    },
    {
      name: "Aspect object",
      path: "/form/node",
    },
    {
      name: "Interface",
      path: "/form/interface",
    },
    {
      name: "Terminal",
      path: "/form/terminal",
    },
    {
      name: "Transport",
      path: "/form/transport",
    },
  ];
};

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

export const useSearchResults = (
  query: string,
  filters: Filter[],
  pageSize = 20
): [results: SearchResult[], totalHits: number, isLoading: boolean] => {
  const [searchItems, isLoading] = useSearchItems();
  const fuseResult = useFuse(searchItems, query, { keys: searchKeys, matchAllOnEmpty: true });

  const results = fuseResult.map((x) => x.item);
  const filtered = filterSearchResults(filters, results);
  const sliced = filtered.slice(0, pageSize);
  const mapped = mapSearchResults(sliced);

  return [mapped, filtered.length, isLoading];
};

export const useGetFilterGroups = (): FilterGroup[] => [getEntityFilters(), getAspectFilters(), useGetPurposeFilters()];

const useGetPurposeFilters = (): FilterGroup => {
  const purposeQuery = useGetPurposes();

  return {
    name: "Purpose",
    filters: purposeQuery.data?.map((p) => ({ key: "purposeName", label: p.name, value: p.name })),
  };
};

const getAspectFilters = (): FilterGroup => {
  const aspectOptions = getValueLabelObjectsFromEnum<Aspect>(Aspect);

  return {
    name: "Aspect",
    filters: aspectOptions.map((a) => ({
      key: "aspect",
      label: a.label,
      value: a.value.toString(),
    })),
  };
};

const getEntityFilters = (): FilterGroup => ({
  name: "Entity",
  filters: [
    {
      key: "kind",
      label: "Attribute",
      value: "AttributeLibCm",
    },
    {
      key: "kind",
      label: "Aspect object",
      value: "NodeLibCm",
    },
    {
      key: "kind",
      label: "Interface",
      value: "InterfaceLibCm",
    },
    {
      key: "kind",
      label: "Terminal",
      value: "TerminalLibCm",
    },
    {
      key: "kind",
      label: "Transport",
      value: "TransportLibCm",
    },
  ],
});

const andFilterItems = (filters: Filter[], items: SearchResultRaw[]) =>
  items.filter((x) => filters.every((f) => x[f.key as keyof SearchResultRaw] == f.value));

const sortItemsByDate = (items: SearchResultRaw[]) =>
  [...items].sort((a, b) => new Date(b.created).getTime() - new Date(a.created).getTime());

/**
 * Filters items with AND-logic if there are any filters available, returns items sorted by date if not.
 *
 * @param filters currently active filters
 * @param items available items after initial search
 */
const filterSearchResults = (filters: Filter[], items: SearchResultRaw[]) => {
  return filters.length > 0 ? andFilterItems(filters, items) : sortItemsByDate(items);
};

const useSearchItems = (): [items: SearchResultRaw[], isLoading: boolean] => {
  const nodeQuery = useGetNodes();
  const terminalQuery = useGetTerminals();
  const attributeQuery = useGetAttributes();
  const transportQuery = useGetTransports();
  const interfaceQuery = useGetInterfaces();

  const isLoading =
    nodeQuery.isLoading ||
    terminalQuery.isLoading ||
    attributeQuery.isLoading ||
    transportQuery.isLoading ||
    interfaceQuery.isLoading;

  const mergedItems = [
    ...(nodeQuery.data ?? []),
    ...(terminalQuery.data ?? []),
    ...(attributeQuery.data ?? []),
    ...(transportQuery.data ?? []),
    ...(interfaceQuery.data ?? []),
  ];

  return [mergedItems, isLoading];
};

const mapSearchResults = (items: SearchResultRaw[]) => {
  const mappedSearchResults: SearchResult[] = [];

  items.forEach((x) => {
    if (isNodeLibCm(x)) mappedSearchResults.push(mapNodeLibCmToNodeItem(x));
    else if (isAttributeLibCm(x)) mappedSearchResults.push(mapAttributeLibCmToAttributeItem(x));
    else if (isTerminalLibCm(x)) mappedSearchResults.push(mapTerminalLibCmToTerminalItem(x));
    else if (isTransportLibCm(x)) mappedSearchResults.push(mapTransportLibCmToTransportItem(x));
    else if (isInterfaceLibCm(x)) mappedSearchResults.push(mapInterfaceLibCmToInterfaceItem(x));
  });

  return mappedSearchResults;
};
