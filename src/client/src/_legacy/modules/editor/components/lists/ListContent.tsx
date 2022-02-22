import { ListElementsContainer } from "../../../../../compLibrary/list";
import { ListType } from "../../TypeEditorList";
import { useMemo } from "react";
import { OnPropertyChangeFunction, OnTerminalCategoryChangeFunction } from "../../types";
import { AttributeType, CreateLibraryType, PredefinedAttribute, Rds, SimpleType, TerminalTypeDict } from "../../../../models";
import {
  AttributeElement,
  LocationAttributeElement,
  ObjectBlockElement,
  PredefinedLocationElement,
  RDSElement,
  SimpleTypeElement,
  TransportInterfaceElement,
} from "./index";
import {
  GetDefaultTerminal,
  GetDefaultTerminals,
  GetFilteredList,
  IsInterface,
  IsTransport,
  RemoveBackground,
  RemoveHover,
  ShowObjectBlock,
  SwitchBackground,
} from "../../helpers";

interface Props {
  disabled?: boolean;
  listType: ListType;
  items: Rds[] | TerminalTypeDict | AttributeType[] | SimpleType[] | PredefinedAttribute[];
  listItems: Rds[] | TerminalTypeDict | AttributeType[] | SimpleType[] | PredefinedAttribute[];
  createLibraryType: CreateLibraryType;
  onPropertyChange?: OnPropertyChangeFunction;
  onTerminalCategoryChange?: OnTerminalCategoryChangeFunction;
}

/**
 * Component that shows content in list based on list type
 * @param interface
 * @returns list-elements based on search and list type
 */
export const ListContent = ({
  disabled,
  listType,
  items,
  listItems,
  createLibraryType,
  onPropertyChange,
  onTerminalCategoryChange,
}: Props) => {
  const filteredList = useMemo(
    () => GetFilteredList(listType, listItems, createLibraryType),
    [listType, listItems, createLibraryType]
  );

  return (
    <>
      {!disabled && (
        <ListElementsContainer
          hover={RemoveHover(listType)}
          background={RemoveBackground(listType)}
          switchBackground={SwitchBackground(listType)}
        >
          {listType === ListType.Rds &&
            filteredList.map((element) => (
              <RDSElement
                key={element.name}
                category={element.name}
                rds={element.items}
                onChange={(key, data) => onPropertyChange(key, data)}
                defaultValue={createLibraryType?.rdsId}
              />
            ))}
          {ShowObjectBlock(listType, createLibraryType)
            ? filteredList.map((element) => (
              <ObjectBlockElement
                key={element.name}
                name={element.name}
                categoryId={element.id}
                defaultTerminals={GetDefaultTerminals(element.id, createLibraryType)}
                terminalTypes={element.items}
                onChange={(key, data) => onTerminalCategoryChange(key, data)}
              />
            ))
            : listType === ListType.Terminals &&
            (IsTransport(createLibraryType.objectType) || IsInterface(createLibraryType.objectType))
              ? filteredList.map((element) => (
                <TransportInterfaceElement
                  key={element.name}
                  categoryName={element.name}
                  terminalTypes={element.items}
                  defaultTerminal={GetDefaultTerminal(listType, createLibraryType, items)}
                  onPropertyChange={onPropertyChange}
                  onTerminalCategoryChange={(key, data) => onTerminalCategoryChange(key, data)}
                />
              ))
              : listType === ListType.PredefinedAttributes &&
              filteredList.map((element) => (
                <PredefinedLocationElement
                  key={element.key}
                  attributeName={element.key}
                  values={element.values}
                  isMultiSelect={element.isMultiSelect}
                  defaultValue={createLibraryType?.predefinedAttributes}
                  onChange={(key, data) => onPropertyChange(key, data)}
                />
              ))}
          {listType === ListType.LocationAttributes
            ? filteredList.map((element) => (
              <LocationAttributeElement
                key={element.id}
                attribute={element}
                onChange={(key, data) => onPropertyChange(key, data)}
                defaultValue={createLibraryType?.attributeTypes}
              />
            ))
            : listType === ListType.ObjectAttributes &&
            filteredList.map((element) => (
              <AttributeElement
                key={element.discipline}
                discipline={element.discipline}
                attributes={element.items}
                onChange={(key, data) => onPropertyChange(key, data)}
                defaultValue={createLibraryType?.attributeTypes}
              />
            ))}
          {listType === ListType.SimpleTypes &&
            filteredList.map((element) => (
              <SimpleTypeElement
                key={element.id}
                simpleType={element}
                onChange={(key, data) => onPropertyChange(key, data)}
                defaultValue={createLibraryType?.simpleTypes}
              />
            ))}
        </ListElementsContainer>
      )}
    </>
  );
};

export default ListContent;
