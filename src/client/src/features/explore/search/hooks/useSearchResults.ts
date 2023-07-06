import { useFuse } from "features/explore/search/hooks/useFuse";
import {
  filterSearchResults,
  mapSearchResults,
  useSearchItems,
} from "features/explore/search/hooks/useSearchResults.helpers";
import { Filter } from "features/explore/search/types/filter";
import { SearchResult } from "features/explore/search/types/searchResult";

/**
 * Indexed fields that the fuzzy-search will try to match a query against
 */
const searchKeys = ["id", "name", "description", "aspect", "attribute"];

export const useSearchResults = (
  query: string,
  filters: Filter[],
  pageSize = 20,
  pageNum = 1,
): [results: SearchResult[], totalHits: number, isLoading: boolean] => {
  const [searchItems, isLoading] = useSearchItems();
  const fuseResult = useFuse(searchItems, query, { keys: searchKeys, matchAllOnEmpty: true });

  const results = fuseResult.map((x) => x.item);
  const filtered = filterSearchResults(filters, results);
  const sliced = filtered.slice((pageNum - 1) * pageSize, pageNum * pageSize);
  const mapped = mapSearchResults(sliced);

  return [mapped, filtered.length, isLoading];
};
