import { mapBlockLibCmToBlockItem, mapTerminalLibCmToTerminalItem } from "common/utils/mappers";
import { useGetBlocks } from "external/sources/block/block.queries";
import { useGetTerminals } from "external/sources/terminal/terminal.queries";
import { Filter } from "features/explore/search/types/filter";
import { SearchResult, SearchResultRaw } from "features/explore/search/types/searchResult";
import { useGetAttributes } from "../../../../external/sources/attribute/attribute.queries";
import { toAttributeItem } from "../../../../common/utils/mappers/mapAttributeLibCmToAttributeItem";
import { useGetUnits } from "../../../../external/sources/unit/unit.queries";
import { useGetQuantityDatums } from "../../../../external/sources/datum/quantityDatum.queries";
import { useGetAllRds } from "../../../../external/sources/rds/rds.queries";
import {
  isBlockLibCm,
  isAttributeLibCm,
  isQuantityDatumLibCm,
  isRdsLibCm,
  isTerminalLibCm,
  isUnitLibCm,
  isAttributeGroupLibCm,
} from "../guards/isItemValidators";
import { toUnitItem } from "../../../../common/utils/mappers/toUnitItem";
import { toQuantityDatumItem } from "../../../../common/utils/mappers/toQuantityDatumItem";
import { toRdsItem } from "../../../../common/utils/mappers/toRdsItem";
import { useGetAttributeGroups } from "external/sources/attributeGroup/attributeGroup.queries";
import { toAttributeGroupItem } from "common/utils/mappers/mapAttributeGroupLibCmToAttributeGroupItem";

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
  [...items].sort((a, b) => new Date(b.created).getTime() - new Date(a.created).getTime());

export const useSearchItems = (): [items: SearchResultRaw[], isLoading: boolean] => {
  const blockQuery = useGetBlocks();
  const terminalQuery = useGetTerminals();
  const attributeQuery = useGetAttributes();
  const unitQuery = useGetUnits();
  const datumQuery = useGetQuantityDatums();
  const rdsQuery = useGetAllRds();
  const attributeGroupsQuery = useGetAttributeGroups();

  const isLoading =
    blockQuery.isLoading ||
    terminalQuery.isLoading ||
    attributeQuery.isLoading ||
    attributeGroupsQuery.isLoading ||
    unitQuery.isLoading ||
    datumQuery.isLoading ||
    rdsQuery.isLoading;

  const mergedItems = [
    ...(blockQuery.data ?? []),
    ...(terminalQuery.data ?? []),
    //...(attributeQuery.data ?? []),
    ...(attributeGroupsQuery.data ?? []),
    //...(unitQuery.data ?? []),
    ...(datumQuery.data ?? []),
    ...(rdsQuery.data ?? []),
  ];

  return [mergedItems, isLoading];
};

export const mapSearchResults = (items: SearchResultRaw[]) => {
  const mappedSearchResults: SearchResult[] = [];

  items.forEach((x) => {
    if (isBlockLibCm(x)) mappedSearchResults.push(mapBlockLibCmToBlockItem(x));
    else if (isTerminalLibCm(x)) mappedSearchResults.push(mapTerminalLibCmToTerminalItem(x));
    else if (isAttributeLibCm(x)) mappedSearchResults.push(toAttributeItem(x));
    else if (isUnitLibCm(x)) mappedSearchResults.push(toUnitItem(x));
    else if (isQuantityDatumLibCm(x)) mappedSearchResults.push(toQuantityDatumItem(x));
    else if (isRdsLibCm(x)) mappedSearchResults.push(toRdsItem(x));
    else if (isAttributeGroupLibCm(x)) mappedSearchResults.push(toAttributeGroupItem(x));
  });

  return mappedSearchResults;
};
