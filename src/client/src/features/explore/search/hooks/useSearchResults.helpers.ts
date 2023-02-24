import { mapNodeLibCmToNodeItem, mapTerminalLibCmToTerminalItem } from "common/utils/mappers";
import { useGetNodes } from "external/sources/node/node.queries";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { isNodeLibCm, isTerminalLibCm } from "features/explore/search/guards";
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

  const isLoading = nodeQuery.isLoading || terminalQuery.isLoading;

  const mergedItems = [...(nodeQuery.data ?? []), ...(terminalQuery.data ?? [])];

  return [mergedItems, isLoading];
};

export const mapSearchResults = (items: SearchResultRaw[]) => {
  const mappedSearchResults: SearchResult[] = [];

  items.forEach((x) => {
    if (isNodeLibCm(x)) mappedSearchResults.push(mapNodeLibCmToNodeItem(x));
    else if (isTerminalLibCm(x)) mappedSearchResults.push(mapTerminalLibCmToTerminalItem(x));
  });

  return mappedSearchResults;
};
