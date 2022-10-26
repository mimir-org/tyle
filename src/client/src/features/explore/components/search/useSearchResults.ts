import { Filter } from "../../types/filter";
import { SearchResult } from "../../types/searchResult";
import { useFuse } from "./useFuse";
import { filterSearchResults, mapSearchResults, useSearchItems } from "./useSearchResults.helpers";

/**
 * Indexed fields that the fuzzy-search will try to match a query against
 */
const searchKeys = ["id", "name", "description", "aspect", "nodeTerminals.terminal.name", "companyName"];

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
