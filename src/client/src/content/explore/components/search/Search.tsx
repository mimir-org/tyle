import { XCircle } from "@styled-icons/heroicons-outline";
import { Fragment } from "react";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Token } from "../../../../complib/general";
import { Flexbox, MotionFlexbox } from "../../../../complib/layouts";
import { MotionText } from "../../../../complib/text";
import { useDebounceState } from "../../../../hooks/useDebounceState";
import { SearchField } from "../../../common/SearchField";
import { SelectedInfo } from "../../types/selectedInfo";
import { ExploreSection } from "../ExploreSection";
import { ConditionalAttributeSearchItem } from "./components/attribute/ConditionalAttributeSearchItem";
import { FilterMenu } from "./components/filter/FilterMenu";
import { ConditionalInterfaceSearchItem } from "./components/interface/ConditionalInterfaceSearchItem";
import { ItemList } from "./components/item/ItemList";
import { LinkMenu } from "./components/link/LinkMenu";
import { ConditionalNodeSearchItem } from "./components/node/ConditionalNodeSearchItem";
import { SearchPlaceholder } from "./components/SearchPlaceholder";
import { ConditionalTerminalSearchItem } from "./components/terminal/ConditionalTerminalSearchItem";
import { ConditionalTransportSearchItem } from "./components/transport/ConditionalTransportSearchItem";
import { getCreateMenuLinks, useFilterState, useGetFilterGroups, useSearchResults } from "./Search.helpers";

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
  const { t } = useTranslation("translation", { keyPrefix: "search" });
  const [activeFilters, toggleFilter] = useFilterState([]);
  const [query, setQuery, debouncedQuery] = useDebounceState("");
  const [results, totalHits, isLoading] = useSearchResults(debouncedQuery, activeFilters, pageLimit);

  const showSearchText = !isLoading;
  const showResults = results.length > 0;
  const showFilterTokens = activeFilters.length > 0;
  const showPlaceholder = !isLoading && results.length === 0;
  const shown = totalHits < pageLimit ? totalHits : pageLimit;

  return (
    <ExploreSection title={t("title")}>
      <Flexbox gap={theme.tyle.spacing.xxxl} alignItems={"center"}>
        <SearchField value={query} onChange={(e) => setQuery(e.target.value)} placeholder={t("placeholders.search")} />
        <FilterMenu
          name={t("filter.title")}
          filterGroups={useGetFilterGroups()}
          activeFilters={activeFilters}
          toggleFilter={toggleFilter}
        />
        <LinkMenu name={"Create"} links={getCreateMenuLinks()} />
      </Flexbox>

      {showFilterTokens && (
        <MotionFlexbox layout={"position"} flexWrap={"wrap"} gap={theme.tyle.spacing.base}>
          {activeFilters.map((x, i) => (
            <Token
              key={i}
              actionable
              actionText={t("filter.templates.remove", { object: x.label })}
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
          {t("templates.hits", { shown: shown, total: totalHits })}
        </MotionText>
      )}

      {showResults && (
        <ItemList>
          {results.map((item) => (
            <Fragment key={item.id}>
              <ConditionalNodeSearchItem
                item={item}
                isSelected={item.id == selected?.id}
                setSelected={() => setSelected({ id: item.id, type: "node" })}
              />
              <ConditionalAttributeSearchItem
                item={item}
                isSelected={item.id == selected?.id}
                setSelected={() => setSelected({ id: item.id, type: "attribute" })}
              />
              <ConditionalTerminalSearchItem
                item={item}
                isSelected={item.id == selected?.id}
                setSelected={() => setSelected({ id: item.id, type: "terminal" })}
              />
              <ConditionalTransportSearchItem
                item={item}
                isSelected={item.id == selected?.id}
                setSelected={() => setSelected({ id: item.id, type: "transport" })}
              />
              <ConditionalInterfaceSearchItem
                item={item}
                isSelected={item.id == selected?.id}
                setSelected={() => setSelected({ id: item.id, type: "interface" })}
              />
            </Fragment>
          ))}
        </ItemList>
      )}

      {showPlaceholder && (
        <SearchPlaceholder
          title={t("help.templates.query", { query })}
          subtitle={t("help.subtitle")}
          tips={[t("help.tip1"), t("help.tip2"), t("help.tip3")]}
        />
      )}
    </ExploreSection>
  );
};
