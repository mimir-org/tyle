import { Accordion } from "@mimirorg/component-library";
import { AdjustmentsHorizontal } from "@styled-icons/heroicons-outline";
import Box from "components/Box";
import Button from "components/Button";
import Popover from "components/Popover";
import SearchField from "components/SearchField";
import { useState } from "react";
import { useTheme } from "styled-components";
import { FilterGroup } from "types/filterGroup";
import { filterAvailableFilters } from "./FilterMenu.helpers";
import FilterMenuGroup, { FilterMenuGroupProps } from "./FilterMenuGroup";

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
const FilterMenu = ({ name, filterGroups, ...delegated }: FilterMenuProps) => {
  const theme = useTheme();
  const [filterQuery, setFilterQuery] = useState("");

  return (
    <Popover
      align={"end"}
      placement={"bottom"}
      bgColor={theme.tyle.color.background.base}
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

export default FilterMenu;
