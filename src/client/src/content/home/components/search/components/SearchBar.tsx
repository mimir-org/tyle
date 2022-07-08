import { PlusSm } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../../assets/text";
import { Button } from "../../../../../complib/buttons";
import { Flexbox } from "../../../../../complib/layouts";
import { SearchField } from "../../../../common/SearchField";

export interface SearchBarProps {
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
    <Flexbox gap={theme.tyle.spacing.xxxl} alignItems={"center"}>
      <SearchField
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        placeholder={TextResources.SEARCH_PLACEHOLDER}
      />
      <Button disabled icon={<PlusSm style={{ flexShrink: 0 }} />}>
        {TextResources.SEARCH_FILTER}
      </Button>
    </Flexbox>
  );
};
