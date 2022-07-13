import { useTheme } from "styled-components";
import { Checkbox } from "../../../../../../complib/inputs/checkbox/Checkbox";
import { Box } from "../../../../../../complib/layouts";
import { AccordionContent, AccordionItem, AccordionTrigger } from "../../../../../../complib/surfaces";
import { Filter } from "../../../../types/Filter";
import { FilterMenuGroupLabel } from "./FilterMenuGroup.styled";

export interface FilterMenuGroupProps {
  name: string;
  filters?: Filter[];
  activeFilters?: Filter[];
  toggleFilter: (value: Filter) => void;
}

/**
 * Component displays a single filter dropdown in the FilterMenu component
 *
 * @param name name for the group of filters that the component will represent
 * @param filters filter objects for the given group
 * @param activeFilters all filters being applied to the search
 * @param toggleFilter function of toggling a single filter on or off
 * @constructor
 */
export const FilterMenuGroup = ({ name, filters, activeFilters, toggleFilter }: FilterMenuGroupProps) => {
  const theme = useTheme();

  return (
    <AccordionItem value={name}>
      <AccordionTrigger>{name}</AccordionTrigger>
      <AccordionContent>
        <Box
          display={"flex"}
          flexDirection={"column"}
          gap={theme.tyle.spacing.xs}
          overflow={"auto"}
          maxHeight={"300px"}
        >
          {filters?.map((f) => {
            return (
              <FilterMenuGroupLabel key={f.label}>
                <Checkbox onClick={() => toggleFilter(f)} checked={activeFilters?.some((x) => x.value === f.value)} />
                {f.label}
              </FilterMenuGroupLabel>
            );
          })}
        </Box>
      </AccordionContent>
    </AccordionItem>
  );
};
