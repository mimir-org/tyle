import { XCircle } from "@styled-icons/heroicons-outline";
import { useDebounceState } from "common/hooks/useDebounceState";
import { mapMimirorgUserCmToUserItem } from "common/utils/mappers/mapMimirorgUserCmToUserItem";
import { Token } from "complib/general";
import { Flexbox, MotionFlexbox } from "complib/layouts";
import { MotionText } from "complib/text";
import { useGetCurrentUser } from "external/sources/user/user.queries";
import { SearchField } from "features/common/search-field";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { FilterMenu } from "features/explore/search/components/filter/FilterMenu";
import { ItemList } from "features/explore/search/components/item/ItemList";
import { LinkMenu } from "features/explore/search/components/link/LinkMenu";
import { SearchPlaceholder } from "features/explore/search/components/SearchPlaceholder";
import { useFilterState } from "features/explore/search/hooks/useFilterState";
import { useGetFilterGroups } from "features/explore/search/hooks/useGetFilterGroups";
import { useSearchResults } from "features/explore/search/hooks/useSearchResults";
import { isPositiveInt, useCreateMenuLinks } from "features/explore/search/Search.helpers";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { SearchResultsRenderer } from "./SearchResultsRenderer";
import { useSearchParams } from "react-router-dom";
import { SearchNavigation } from "./SearchNavigation";

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
export const Search = ({ selected, setSelected, pageLimit = 20 }: SearchProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const createMenuLinks = useCreateMenuLinks();
  const [activeFilters, toggleFilter] = useFilterState([]);
  const [query, setQuery, debouncedQuery] = useDebounceState("");
  const [searchParams, setSearchParams] = useSearchParams();
  const pageParam = searchParams.get("page");
  const userQuery = useGetCurrentUser();
  const user = userQuery?.data != null ? mapMimirorgUserCmToUserItem(userQuery.data) : undefined;
  const [results, totalHits, isLoading] = useSearchResults(debouncedQuery, activeFilters, pageLimit, Number(pageParam));

  if (!isPositiveInt(pageParam) || (!isLoading && Number(pageParam) > Math.ceil(totalHits / pageLimit))) {
    setSearchParams({ page: "1" });
  }

  const showSearchText = !isLoading;
  const showResults = results.length > 0;
  const showNavigation = totalHits > pageLimit;
  const showFilterTokens = activeFilters.length > 0;
  const showPlaceholder = !isLoading && results.length === 0;
  const lowerShown = (Number(pageParam) - 1) * pageLimit + 1;
  const higherShown = Math.min(Number(pageParam) * pageLimit, totalHits);
  const shown = totalHits < pageLimit ? totalHits : lowerShown <= higherShown ? lowerShown + "–" + higherShown : 0;

  return (
    <ExploreSection title={t("search.title")}>
      <Flexbox gap={theme.tyle.spacing.xxxl} alignItems={"center"}>
        <SearchField
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder={t("search.placeholders.search")}
        />
        <FilterMenu
          name={t("search.filter.title")}
          filterGroups={useGetFilterGroups()}
          activeFilters={activeFilters}
          toggleFilter={toggleFilter}
        />
        <LinkMenu name={t("search.create.title")} links={createMenuLinks} justifyContent={"space-between"} />
      </Flexbox>

      {showFilterTokens && (
        <MotionFlexbox layout={"position"} flexWrap={"wrap"} gap={theme.tyle.spacing.base}>
          {activeFilters.map((x) => (
            <Token
              key={`${x.value}`}
              actionable
              actionText={t("search.filter.templates.remove", { object: x.label })}
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
          {t("search.templates.hits", { shown: shown, total: totalHits })}
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
          title={t("search.help.templates.query", { query })}
          subtitle={t("search.help.subtitle")}
          tips={[t("search.help.tip1"), t("search.help.tip2"), t("search.help.tip3")]}
        />
      )}
    </ExploreSection>
  );
};
