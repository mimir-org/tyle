import { mapAspectObjectLibCmToAspectObjectItem, mapTerminalLibCmToTerminalItem } from "common/utils/mappers";
import { useGetAspectObjects } from "external/sources/aspectobject/aspectObject.queries";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { isAspectObjectLibCm, isTerminalLibCm } from "features/explore/search/guards";
import { Filter } from "features/explore/search/types/filter";
import { SearchResult, SearchResultRaw } from "features/explore/search/types/searchResult";
import { useGetAttributes } from "../../../../external/sources/attribute/attribute.queries";
import { isAttributeLibCm } from "../guards/isAttributeLibCm";
import { toAttributeItem } from "../../../../common/utils/mappers/mapAttributeLibCmToAttributeItem";

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
  const aspectObjectQuery = useGetAspectObjects();
  const terminalQuery = useGetTerminals();
  const attributeQuery = useGetAttributes();

  const isLoading = aspectObjectQuery.isLoading || terminalQuery.isLoading || attributeQuery.isLoading;

  const mergedItems = [
    ...(aspectObjectQuery.data ?? []),
    ...(terminalQuery.data ?? []),
    ...(attributeQuery.data ?? []),
  ];

  return [mergedItems, isLoading];
};

export const mapSearchResults = (items: SearchResultRaw[]) => {
  const mappedSearchResults: SearchResult[] = [];

  items.forEach((x) => {
    if (isAspectObjectLibCm(x)) mappedSearchResults.push(mapAspectObjectLibCmToAspectObjectItem(x));
    else if (isTerminalLibCm(x)) mappedSearchResults.push(mapTerminalLibCmToTerminalItem(x));
    else if (isAttributeLibCm(x)) mappedSearchResults.push(toAttributeItem(x));
  });

  return mappedSearchResults;
};
