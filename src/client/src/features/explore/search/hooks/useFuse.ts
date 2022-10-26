import Fuse from "fuse.js";
import { useMemo } from "react";

export type UseFuseOptions<T> = Fuse.IFuseOptions<T> & { matchAllOnEmpty?: boolean };

/**
 * Hook for using Fuse in combination with useMemo to reduce performance impact
 *
 * @see https://fusejs.io
 * @see https://fusejs.io/api/options
 *
 * @param items that will be indexed by fuse
 * @param query that will be searched for
 * @param options that adjust fuse behaviour
 */
export const useFuse = <T>(items: T[], query?: string | Fuse.Expression, options?: UseFuseOptions<T>) => {
  const fuseClient = useMemo(() => new Fuse(items, options), [items, options]);
  const showAllItems = !query && options?.matchAllOnEmpty;

  return useMemo(
    () => (showAllItems ? mapSourceItemsToFuseResults(items) : fuseClient.search(query ?? "")),
    [fuseClient, items, query, showAllItems]
  );
};

const mapSourceItemsToFuseResults = <T>(items: T[]) =>
  items.map<Fuse.FuseResult<T>>((item, index) => ({ item, refIndex: index, matches: [], score: 1 }));
