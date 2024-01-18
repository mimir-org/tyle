import { AccordionContent, AccordionItem, AccordionTrigger } from "components/Accordion";
import Box from "components/Box";
import Checkbox from "components/Checkbox";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { Filter } from "types/filter";
import FilterMenuGroupLabel from "./FilterMenuGroup.styled";

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
const FilterMenuGroup = ({ name, filters, activeFilters, toggleFilter }: FilterMenuGroupProps) => {
  const theme = useTheme();

  return (
    <AccordionItem value={name}>
      <AccordionTrigger>{name}</AccordionTrigger>
      <AccordionContent>
        <Box
          display={"flex"}
          flexDirection={"column"}
          gap={theme.tyle.spacing.xs}
          maxHeight={"300px"}
          overflow={"auto"}
        >
          {filters?.map((f, i) => (
            <FilterMenuGroupLabel key={`${i + f.label + f.key}`}>
              <Checkbox onClick={() => toggleFilter(f)} checked={activeFilters?.some((x) => x.value === f.value)} />
              <Text as={"span"} color={theme.tyle.color.pure.on}>
                {f.label}
              </Text>
            </FilterMenuGroupLabel>
          ))}
        </Box>
      </AccordionContent>
    </AccordionItem>
  );
};

export default FilterMenuGroup;
