import { useState } from "react";
import { ListWrapper } from "../../../compLibrary/list";
import { ListContent, ListSearch } from "./components/lists";
import { GetFlexForListType, GetListLabel } from "./helpers";
import { AttributeType, CreateLibraryType, PredefinedAttribute, Rds, SimpleType, TerminalTypeDict } from "../../models";
import { OnPropertyChangeFunction, OnTerminalCategoryChangeFunction } from "./types";

export enum ListType {
  Rds = 0,
  Terminals = 1,
  PredefinedAttributes = 2,
  ObjectAttributes = 3,
  LocationAttributes = 4,
  SimpleTypes = 5,
}

export interface TypeEditorListProps {
  createLibraryType: CreateLibraryType;
  items: Rds[] | TerminalTypeDict | AttributeType[] | SimpleType[] | PredefinedAttribute[];
  disabled?: boolean;
  listType: ListType;
  onPropertyChange?: OnPropertyChangeFunction;
  onTerminalCategoryChange?: OnTerminalCategoryChangeFunction;
}

/**
 * A generic list-component in Type editor
 * @returns a visual Type Editor list
 */
export const TypeEditorList = ({
  createLibraryType,
  items,
  disabled,
  listType,
  onPropertyChange,
  onTerminalCategoryChange,
}: TypeEditorListProps) => {
  const [filteredListItems, setListItems] = useState(items);
  return (
    <ListWrapper flex={GetFlexForListType(listType)} disabled={disabled} minHeight={'100%'}>
      <ListSearch
        listType={listType}
        placeHolder={GetListLabel(listType, createLibraryType)}
        list={items}
        setlistItems={setListItems}
      />
      <ListContent
        disabled={disabled}
        listType={listType}
        items={items}
        listItems={filteredListItems}
        createLibraryType={createLibraryType}
        onPropertyChange={(key, data) => onPropertyChange(key, data)}
        onTerminalCategoryChange={(key, data) => onTerminalCategoryChange(key, data)}
      />
    </ListWrapper>
  );
};

export default TypeEditorList;
