import { useState } from "react";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../assets/text";
import textResources from "../../../../assets/text/TextResources";
import { MotionFlexbox } from "../../../../complib/layouts";
import { MotionText, Text } from "../../../../complib/text";
import { useGetNodes } from "../../../../data/queries/tyle/queriesNode";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";
import { HomeSection } from "../HomeSection";
import { ItemList } from "./components/item/ItemList";
import { NodeSearchItem } from "./components/node/NodeSearchItem";
import { SearchBar } from "./components/SearchBar";
import { filterSearchItem } from "./Search.helpers";

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
  const results =
    nodeQuery.data?.map((n) => mapNodeLibCmToNodeItem(n)).filter((n) => filterSearchItem(n.name, searchQuery)) ?? [];

  const showResults = results.length > 0;
  const showSearchText = !nodeQuery.isLoading;
  const showPlaceholder = !nodeQuery.isLoading && results.length === 0;

  return (
    <HomeSection title={TextResources.SEARCH_TITLE}>
      <SearchBar searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
      {showSearchText && (
        <MotionText
          variant={"label-large"}
          color={theme.tyle.color.sys.surface.variant.on}
          {...theme.tyle.animation.fade}
        >
          {results.length} {textResources.SEARCH_RESULTS}
        </MotionText>
      )}
      {showResults && (
        <ItemList>
          {results.map((n) => (
            <NodeSearchItem key={n.id} isSelected={n.id === selected} setSelected={() => setSelected(n.id)} {...n} />
          ))}
        </ItemList>
      )}
      {showPlaceholder && <Placeholder searchQuery={searchQuery} />}
    </HomeSection>
  );
};

const Placeholder = ({ searchQuery }: { searchQuery: string }) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flexDirection={"column"} gap={theme.tyle.spacing.xl} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.sys.surface.variant.on} wordBreak={"break-all"}>
        {textResources.SEARCH_HELP_TITLE} “{searchQuery}”
      </Text>
      <Text variant={"label-large"} color={theme.tyle.color.sys.primary.base}>
        {textResources.SEARCH_HELP_SUBTITLE}
      </Text>
      <ul>
        <li>{textResources.SEARCH_HELP_TIP_1}</li>
        <li>{textResources.SEARCH_HELP_TIP_2}</li>
        <li>{textResources.SEARCH_HELP_TIP_3}</li>
      </ul>
    </MotionFlexbox>
  );
};
