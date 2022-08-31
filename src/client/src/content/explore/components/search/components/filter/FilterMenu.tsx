import { PlusSm } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { useTheme } from "styled-components";
import { Button } from "../../../../../../complib/buttons";
import { Popover } from "../../../../../../complib/data-display";
import { Box } from "../../../../../../complib/layouts";
import { Accordion } from "../../../../../../complib/surfaces";
import { SearchField } from "../../../../../common/SearchField";
import { FilterGroup } from "../../../../types/filterGroup";
import { filterAvailableFilters } from "./FilterMenu.helpers";
import { FilterMenuGroup, FilterMenuGroupProps } from "./FilterMenuGroup";

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
            {filterGroups.map((x) => (
              <FilterMenuGroup
                key={x.name}
                name={x.name}
                filters={filterAvailableFilters(filterQuery, x.filters)}
                {...delegated}
              />
            ))}
          </Accordion>
        </Box>
      }
    >
      <Button icon={<PlusSm size={24} />} flexShrink={"0"}>
        {name}
      </Button>
    </Popover>
  );
};
