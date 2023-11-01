import { useGetAttributes } from "api/attribute.queries";
import { useGetBlocks } from "api/block.queries";
import { useGetTerminals } from "api/terminal.queries";
import { Filter } from "common/types/filter";
import { toBlockItem } from "common/utils/mappers/mapBlockLibCmToBlockItem";
import { toAttributeItem } from "components/Search/mapAttributeLibCmToAttributeItem";
import { toTerminalItem } from "components/Search/mapTerminalLibCmToTerminalItem";
import { SearchResult, SearchResultRaw } from "components/Search/searchResult";
import { isAttributeView, isBlockView, isTerminalView } from "./isItemValidators";

/**
 * Filters items if there are any filters available, returns items sorted by date if not.
 *
 * @param filters currently active filters
 * @param items available items after initial search
 */
export const filterSearchResults = (filters: Filter[], items: SearchResultRaw[]) => {
  return filters.length > 0 ? filterItems(filters, items) : sortItemsByDate(items);
};

/**
 * Filters items with OR-logic per key present in filters, then returns the intersection of the results.
 *
 * @param filters currently active filters
 * @param items available items after initial search
 */
const filterItems = (filters: Filter[], items: SearchResultRaw[]) => {
  const filterKeys = Array.from(new Set(filters.map((f) => f.key)));
  const filteredPerKey: SearchResultRaw[][] = [];
  for (const key of filterKeys) {
    filteredPerKey.push(
      orFilterItems(
        filters.filter((f) => f.key === key),
        items,
      ),
    );
  }

  return sortItemsByDate(intersect(filteredPerKey));
};

/**
 * Returns the intersection of the arrays in the input array.
 *
 * @param arrayOfArrays an array of the arrays that should be intersected
 */
const intersect = (arrayOfArrays: SearchResultRaw[][]): SearchResultRaw[] => {
  if (arrayOfArrays.length === 1) return arrayOfArrays[0];

  return arrayOfArrays.reduce((a, b) => a.filter((c) => b.includes(c)));
};

/**
 * Filters items using OR-logic.
 * @param filters currently active filters
 * @param items available items after initial search
 */
const orFilterItems = (filters: Filter[], items: SearchResultRaw[]) =>
  items.filter((x) => filters.some((f) => String(x[f.key as keyof SearchResultRaw]) === f.value));

const sortItemsByDate = (items: SearchResultRaw[]) =>
  [...items].sort((a, b) => new Date(b.createdOn).getTime() - new Date(a.createdOn).getTime());

export const useSearchItems = (): [items: SearchResultRaw[], isLoading: boolean] => {
  const blockQuery = useGetBlocks();
  const terminalQuery = useGetTerminals();
  const attributeQuery = useGetAttributes();
  //const attributeGroupsQuery = useGetAttributeGroups();

  const isLoading = blockQuery.isLoading || terminalQuery.isLoading || attributeQuery.isLoading; // ||
  //attributeGroupsQuery.isLoading;

  const mergedItems = [
    ...(blockQuery.data ?? []),
    ...(terminalQuery.data ?? []),
    ...(attributeQuery.data ?? []),
    //...(attributeGroupsQuery.data ?? []),
  ];

  return [mergedItems, isLoading];
};

export const mapSearchResults = (items: SearchResultRaw[]) => {
  const mappedSearchResults: SearchResult[] = [];

  items.forEach((x) => {
    if (isBlockView(x)) mappedSearchResults.push(toBlockItem(x));
    else if (isTerminalView(x)) mappedSearchResults.push(toTerminalItem(x));
    else if (isAttributeView(x)) mappedSearchResults.push(toAttributeItem(x));
    //else if (isAttributeGroupLibCm(x)) mappedSearchResults.push(toAttributeGroupItem(x));
  });

  return mappedSearchResults;
};
