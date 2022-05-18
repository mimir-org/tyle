import { useState } from "react";
import { useTheme } from "styled-components";
import { Box } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { ItemList } from "./components/item/ItemList";
import { useGetNodes } from "../../../../data/queries/tyle/queriesNode";
import { TextResources } from "../../../../assets/text";
import { Item } from "./components/item/Item";
import { SearchBar } from "./components/SearchBar";
import { filterSearchItem } from "./Search.helpers";
import { mapNodeLibCmToSearchItem } from "../../../../utils/mappers";

interface SearchProps {
  selected?: string;
  setSelected: (item: string) => void;
}

/**
 * Component which displays a search bar and its associated search results.
 *
 * @param selected the id of the search result which has been selected
 * @param setSelected function for setting the id of the selected search result item
 * @constructor
 */
export const Search = ({ selected, setSelected }: SearchProps) => {
  const theme = useTheme();
  const nodeQuery = useGetNodes();
  const [searchQuery, setSearchQuery] = useState("");

  return (
    <Box
      as={"section"}
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.large}
      pt={theme.tyle.spacing.xl}
      px={theme.tyle.spacing.large}
      pb={theme.tyle.spacing.medium}
      height={"100%"}
      minWidth={"400px"}
    >
      <Text variant={"display-small"}>{TextResources.SEARCH_TITLE}</Text>
      <SearchBar searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
      {nodeQuery.isSuccess && !nodeQuery.isLoading && (
        <ItemList>
          {nodeQuery.data
            .map((n) => mapNodeLibCmToSearchItem(n))
            .filter((n) => filterSearchItem(n, searchQuery))
            .map((i) => (
              <Item isSelected={i.id === selected} setSelected={() => setSelected(i.id)} key={i.id} {...i} />
            ))}
        </ItemList>
      )}
    </Box>
  );
};
