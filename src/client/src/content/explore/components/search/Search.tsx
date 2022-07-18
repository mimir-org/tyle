import { XCircle } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../assets/text";
import textResources from "../../../../assets/text/TextResources";
import { Token } from "../../../../complib/general";
import { Flexbox, MotionFlexbox } from "../../../../complib/layouts";
import { MotionText, Text } from "../../../../complib/text";
import { useDebounceState } from "../../../../hooks/useDebounceState";
import { SearchField } from "../../../common/SearchField";
import { ExploreSection } from "../ExploreSection";
import { FilterMenu } from "./components/filter/FilterMenu";
import { ItemList } from "./components/item/ItemList";
import { NodeSearchItem } from "./components/node/NodeSearchItem";
import { useFilterState, useGetFilterGroups, useSearchResults } from "./Search.helpers";

interface SearchProps {
  selected?: string;
  setSelected: (item: string) => void;
}

/**
 * Component which displays search controls and search results.
 *
 * @param selected the id of the search result which has been selected
 * @param setSelected sets the id of the selected search result item
 * @constructor
 */
export const Search = ({ selected, setSelected }: SearchProps) => {
  const theme = useTheme();
  const filterGroups = useGetFilterGroups();
  const [activeFilters, toggleFilter] = useFilterState([]);
  const [query, setQuery, debouncedQuery] = useDebounceState("");
  const [results, isLoading] = useSearchResults(debouncedQuery, activeFilters);

  const showResults = results.length > 0;
  const showFilterTokens = activeFilters.length > 0;
  const showSearchText = !isLoading;
  const showPlaceholder = !isLoading && results.length === 0;

  return (
    <ExploreSection title={TextResources.SEARCH_TITLE}>
      <Flexbox gap={theme.tyle.spacing.xxxl} alignItems={"center"}>
        <SearchField
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder={TextResources.SEARCH_PLACEHOLDER}
        />
        <FilterMenu
          name={TextResources.FILTER_TITLE}
          filterGroups={filterGroups}
          activeFilters={activeFilters}
          toggleFilter={toggleFilter}
        />
      </Flexbox>

      {showFilterTokens && (
        <MotionFlexbox layout={"position"} flexWrap={"wrap"} gap={theme.tyle.spacing.base}>
          {activeFilters.map((x, i) => (
            <Token
              key={i}
              actionable
              actionText={`${textResources.FILTER_REMOVE_START} ${x.label} ${textResources.FILTER_REMOVE_END}`}
              actionIcon={<XCircle />}
              onAction={() => toggleFilter(x)}
            >
              {x.label}
            </Token>
          ))}
        </MotionFlexbox>
      )}

      {showSearchText && (
        <MotionText
          layout
          variant={"label-large"}
          color={theme.tyle.color.sys.surface.variant.on}
          {...theme.tyle.animation.fade}
        >
          {results.length} {textResources.SEARCH_RESULTS}
        </MotionText>
      )}

      {showResults && (
        <ItemList>
          {results.map((nodeItem) => (
            <NodeSearchItem
              key={nodeItem.id}
              isSelected={nodeItem.id === selected}
              setSelected={() => setSelected(nodeItem.id)}
              {...nodeItem}
            />
          ))}
        </ItemList>
      )}

      {showPlaceholder && <Placeholder query={query} />}
    </ExploreSection>
  );
};

const Placeholder = ({ query }: { query: string }) => {
  const theme = useTheme();

  return (
    <MotionFlexbox layout flexDirection={"column"} gap={theme.tyle.spacing.xl} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.sys.surface.variant.on} wordBreak={"break-all"}>
        {textResources.SEARCH_HELP_TITLE} “{query}”
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
