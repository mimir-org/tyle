import { ChangeEvent, useEffect, useState } from "react";
import { CollapseIcon, ExpandIcon } from "../../../assets/icons/chevron";
import { AttributeType } from "../../models";
import { SearchBar, SearchBarContainer, SearchBarList, SearchBarListItem } from "../index";

interface Props<T extends SearchDropDownItem> {
  value?: string;
  placeHolder?: string;
  onChange: (item: T) => void;
  list: T[];
}

export interface SearchDropDownItem {
  id: string;
  name: string;
  attributes: AttributeType[];
}

const SearchDropDown = <T extends SearchDropDownItem>({ value, placeHolder, list, onChange }: Props<T>) => {
  const [isListOpen, setIsListOpen] = useState(false);
  const [searchString, setSearchString] = useState("");
  const isInArray = list.find((x) => x.name === searchString);

  const filter =
    (searchString &&
      searchString.length > 0 &&
      list.filter((x) => x && x.name && x.name.toLowerCase().includes(searchString.toLowerCase()))) ||
    list;

  const valueChanged = (item: T) => {
    setSearchString(item.name);
    setIsListOpen(false);
    onChange(item);
  };

  const showListItems = () => {
    const filteredList = isInArray ? list : filter;
    return filteredList.map((item) => {
      return (
        <SearchBarListItem key={item.id} onClick={() => valueChanged(item)}>
          <p>{item.name}</p>
        </SearchBarListItem>
      );
    });
  };

  useEffect(() => {
    if (value) {
      const listItem = list.find((x) => x.id === value);
      setSearchString(listItem.name);
    }
  }, [value, list]);

  return (
    <SearchBarContainer>
      <SearchBar>
        <label htmlFor="terminalsearch" />
        <input
          type="text"
          value={searchString}
          placeholder={placeHolder ?? ""}
          onChange={(e: ChangeEvent<HTMLInputElement>) => setSearchString(e.target.value)}
          onFocus={() => setIsListOpen(!isListOpen)}
        />
        <img
          src={isListOpen ? ExpandIcon : CollapseIcon}
          alt="expand-icon"
          onClick={() => {
            setIsListOpen(!isListOpen);
          }}
          className="icon"
        />
      </SearchBar>
      {isListOpen && list && <SearchBarList>{showListItems()}</SearchBarList>}
    </SearchBarContainer>
  );
};

export default SearchDropDown;
