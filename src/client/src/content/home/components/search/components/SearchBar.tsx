import { Input } from "../../../../../complib/inputs";
import { TextResources } from "../../../../../assets/text";
import { Button } from "../../../../../complib/buttons";
import { Filter } from "@styled-icons/heroicons-outline";
import { Flexbox } from "../../../../../complib/layouts";
import { useTheme } from "styled-components";

interface SearchBarProps {
  searchQuery?: string;
  setSearchQuery: (value: string) => void;
}

/**
 * Component with a search field and filter options.
 *
 * @param searchQuery the current search query
 * @param setSearchQuery function for setting the search query
 * @constructor
 */
export const SearchBar = ({ searchQuery, setSearchQuery }: SearchBarProps) => {
  const theme = useTheme();

  return (
    <Flexbox gap={theme.tyle.spacing.large} alignItems={"center"}>
      <Input
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        placeholder={TextResources.SEARCH_PLACEHOLDER}
      />
      <Button disabled icon={<Filter />}>
        {TextResources.SEARCH_FILTER}
      </Button>
    </Flexbox>
  );
};
