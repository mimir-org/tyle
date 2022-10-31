import {
  mapInterfaceLibCmToInterfaceItem,
  mapNodeLibCmToNodeItem,
  mapTerminalLibCmToTerminalItem,
  mapTransportLibCmToTransportItem,
} from "common/utils/mappers";
import { useGetInterfaces } from "external/sources/interface/interface.queries";
import { useGetNodes } from "external/sources/node/node.queries";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { useGetTransports } from "external/sources/transport/transport.queries";
import { isInterfaceLibCm, isNodeLibCm, isTerminalLibCm, isTransportLibCm } from "features/explore/search/guards";
import { Filter } from "features/explore/search/types/filter";
import { SearchResult, SearchResultRaw } from "features/explore/search/types/searchResult";

/**
 * Filters items with AND-logic if there are any filters available, returns items sorted by date if not.
 *
 * @param filters currently active filters
 * @param items available items after initial search
 */
export const filterSearchResults = (filters: Filter[], items: SearchResultRaw[]) => {
  return filters.length > 0 ? andFilterItems(filters, items) : sortItemsByDate(items);
};

const andFilterItems = (filters: Filter[], items: SearchResultRaw[]) =>
  items.filter((x) => filters.every((f) => x[f.key as keyof SearchResultRaw] == f.value));

const sortItemsByDate = (items: SearchResultRaw[]) =>
  [...items].sort((a, b) => new Date(b.created).getTime() - new Date(a.created).getTime());

export const useSearchItems = (): [items: SearchResultRaw[], isLoading: boolean] => {
  const nodeQuery = useGetNodes();
  const terminalQuery = useGetTerminals();
  const transportQuery = useGetTransports();
  const interfaceQuery = useGetInterfaces();

  const isLoading =
    nodeQuery.isLoading || terminalQuery.isLoading || transportQuery.isLoading || interfaceQuery.isLoading;

  const mergedItems = [
    ...(nodeQuery.data ?? []),
    ...(terminalQuery.data ?? []),
    ...(transportQuery.data ?? []),
    ...(interfaceQuery.data ?? []),
  ];

  return [mergedItems, isLoading];
};

export const mapSearchResults = (items: SearchResultRaw[]) => {
  const mappedSearchResults: SearchResult[] = [];

  items.forEach((x) => {
    if (isNodeLibCm(x)) mappedSearchResults.push(mapNodeLibCmToNodeItem(x));
    else if (isTerminalLibCm(x)) mappedSearchResults.push(mapTerminalLibCmToTerminalItem(x));
    else if (isTransportLibCm(x)) mappedSearchResults.push(mapTransportLibCmToTransportItem(x));
    else if (isInterfaceLibCm(x)) mappedSearchResults.push(mapInterfaceLibCmToInterfaceItem(x));
  });

  return mappedSearchResults;
};
