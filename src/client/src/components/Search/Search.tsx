import { Flexbox, MotionFlexbox, MotionText, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetCurrentUser } from "api/user.queries";
import ExploreSection from "components/ExploreSection";
import FilterMenu from "components/FilterMenu";
import LinkMenu from "components/LinkMenu";
import SearchField from "components/SearchField";
import { mapUserViewToUserItem } from "helpers/mappers.helpers";
import { useDebounceState } from "hooks/useDebounceState";
import { useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { useTheme } from "styled-components";
import { SelectedInfo } from "types/selectedInfo";
import ItemList from "./ItemList";
import { isPositiveInt, useCreateMenuLinks } from "./Search.helpers";
import SearchNavigation from "./SearchNavigation";
import SearchPlaceholder from "./SearchPlaceholder";
import SearchResultsRenderer from "./SearchResultsRenderer";
import { useFilterState } from "./useFilterState";
import { useGetFilterGroups } from "./useGetFilterGroups";
import { useSearchResults } from "./useSearchResults";

interface SearchProps {
  selected?: SelectedInfo;
  setSelected: (item: SelectedInfo) => void;
  pageLimit?: number;
}

/**
 * Component which displays search controls and search results.
 *
 * @param selected the id and type of currently selected entity
 * @param setSelected sets the id and type of the selected search result item
 * @param pageLimit how many items to show per "page" (defaults to 20)
 * @constructor
 */
const Search = ({ selected, setSelected, pageLimit = 20 }: SearchProps) => {
  const theme = useTheme();
  const createMenuLinks = useCreateMenuLinks();
  const [activeFilters, toggleFilter] = useFilterState([]);
  const [query, setQuery, debouncedQuery] = useDebounceState("");
  const [searchParams, setSearchParams] = useSearchParams();
  const pageParam = searchParams.get("page");
  const userQuery = useGetCurrentUser();
  const user = userQuery?.data != null ? mapUserViewToUserItem(userQuery.data) : undefined;
  const [results, totalHits, isLoading] = useSearchResults(debouncedQuery, activeFilters, pageLimit, Number(pageParam));

  useEffect(() => {
    if (!isPositiveInt(pageParam) || (!isLoading && Number(pageParam) > Math.ceil(totalHits / pageLimit))) {
      setSearchParams({ page: "1" });
    }
  });

  const showSearchText = !isLoading;
  const showResults = results.length > 0;
  const showNavigation = totalHits > pageLimit;
  const showFilterTokens = activeFilters.length > 0;
  const showPlaceholder = !isLoading && results.length === 0;
  const lowerShown = (Number(pageParam) - 1) * pageLimit + 1;
  const higherShown = Math.min(Number(pageParam) * pageLimit, totalHits);
  const shown = totalHits < pageLimit ? totalHits : lowerShown <= higherShown ? lowerShown + "–" + higherShown : 0;

  return (
    <ExploreSection title="Search">
      <Flexbox gap={theme.mimirorg.spacing.xxxl} alignItems={"center"}>
        <SearchField value={query} onChange={(e) => setQuery(e.target.value)} placeholder="Search for types" />
        <FilterMenu
          name="Filter"
          filterGroups={useGetFilterGroups()}
          activeFilters={activeFilters}
          toggleFilter={toggleFilter}
        />
        <LinkMenu
          name="Create"
          links={createMenuLinks}
          justifyContent={"space-between"}
          disabled={
            user?.roles.filter((x) => x === "Administrator" || x === "Reviewer" || x === "Contributor").length === 0
          }
        />
      </Flexbox>

      {showFilterTokens && (
        <MotionFlexbox layout={"position"} flexWrap={"wrap"} gap={theme.mimirorg.spacing.base}>
          {activeFilters.map((x) => (
            <Token
              key={`${x.value}`}
              actionable
              actionText={`Remove ${x.label} filter`}
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
          color={theme.mimirorg.color.surface.variant.on}
          {...theme.mimirorg.animation.fade}
        >
          {`Showing ${shown} of ${totalHits} results found`}
        </MotionText>
      )}

      {showResults && user && (
        <ItemList>
          {results.map((item) => (
            <SearchResultsRenderer
              key={item.id}
              item={item}
              selectedItemId={selected?.id ?? ""}
              setSelected={setSelected}
              user={user}
            />
          ))}
        </ItemList>
      )}

      {showNavigation && user && <SearchNavigation numPages={Math.ceil(totalHits / pageLimit)} />}

      {showPlaceholder && (
        <SearchPlaceholder
          title={`We are sorry, there are no results for “${query}”`}
          subtitle="Search help"
          tips={[
            "Check your search for typos",
            "Use more generic search terms",
            "The type you are searching for might not have been added yet",
          ]}
        />
      )}
    </ExploreSection>
  );
};

export default Search;
