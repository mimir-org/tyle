import { AdjustmentsHorizontal } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { Box, Popover } from "@mimirorg/component-library";
import { Accordion } from "complib/surfaces";
import { SearchField } from "features/common/search-field";
import { filterAvailableFilters } from "features/explore/search/components/filter/FilterMenu.helpers";
import { FilterMenuGroup, FilterMenuGroupProps } from "features/explore/search/components/filter/FilterMenuGroup";
import { FilterGroup } from "features/explore/search/types/filterGroup";
import { useState } from "react";
import { useTheme } from "styled-components";

export type FilterMenuProps = Omit<FilterMenuGroupProps, "name" | "filters"> & {
  name: string;
  filterGroups: FilterGroup[];
};

/**
 * Component which displays a button that has a filter menu popover
 *
 * @param name text on filter button
 * @param filterGroups available filter-groups
 * @param delegated receives props for FilterGroup
 * @constructor
 */
export const FilterMenu = ({ name, filterGroups, ...delegated }: FilterMenuProps) => {
  const theme = useTheme();
  const [filterQuery, setFilterQuery] = useState("");

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.sys.background.base}
      content={
        <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xl} width={"260px"}>
          <SearchField placeholder={"Search"} value={filterQuery} onChange={(e) => setFilterQuery(e.target.value)} />
          <Accordion>
            {filterGroups.map((x, i) => (
              <FilterMenuGroup
                key={`${i + x.name}`}
                name={x.name}
                filters={filterAvailableFilters(filterQuery, x.filters)}
                {...delegated}
              />
            ))}
          </Accordion>
        </Box>
      }
    >
      <Button icon={<AdjustmentsHorizontal size={24} />} flexShrink={"0"}>
        {name}
      </Button>
    </Popover>
  );
};
