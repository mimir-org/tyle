import { useState } from "react";
import { TextResources } from "../../../../assets/text";
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
  const nodeQuery = useGetNodes();
  const [searchQuery, setSearchQuery] = useState("");
  const showSearchItems = nodeQuery.isSuccess && !nodeQuery.isLoading;

  return (
    <HomeSection title={TextResources.SEARCH_TITLE}>
      <SearchBar searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
      {showSearchItems && (
        <ItemList>
          {nodeQuery.data
            .map((n) => mapNodeLibCmToNodeItem(n))
            .filter((n) => filterSearchItem(n.name, searchQuery))
            .map((n) => (
              <NodeSearchItem key={n.id} isSelected={n.id === selected} setSelected={() => setSelected(n.id)} {...n} />
            ))}
        </ItemList>
      )}
    </HomeSection>
  );
};
