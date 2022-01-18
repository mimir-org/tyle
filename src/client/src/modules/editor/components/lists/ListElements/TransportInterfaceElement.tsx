import { ChangeEvent, useState } from "react";
import { ConnectorType, TerminalType, TerminalTypeItem } from "../../../../../models";
import { ListType, RadioButtonContainer } from "../../inputs/RadioButtonContainer";
import { TerminalCategoryWrapper, TerminalListElement } from "../../../styled";
import { CollapseIcon, ExpandIcon } from "../../../../../assets/icons/chevron";
import { TextResources } from "../../../../../assets/text";
import { SearchBar, SearchBarContainer, SearchBarList, SearchBarListItem } from "../../../../../compLibrary";
import { OnPropertyChangeFunction, OnTerminalCategoryChangeFunction } from "../../../types";
import { CreateId } from "../../../../../helpers";

interface Props {
  categoryName: string;
  terminalTypes: TerminalType[];
  onPropertyChange: OnPropertyChangeFunction;
  onTerminalCategoryChange: OnTerminalCategoryChangeFunction;
  defaultTerminal?: TerminalType;
}

export const TransportInterfaceElement = ({
  categoryName,
  terminalTypes,
  onPropertyChange,
  onTerminalCategoryChange,
  defaultTerminal,
}: Props) => {
  const [searchbarInput, setSearchbarInput] = useState(defaultTerminal ? defaultTerminal.name : "");
  const [expandList, setExpandList] = useState(false);
  const filter = terminalTypes?.filter((t) => t.name.match(new RegExp(searchbarInput, "i")));

  const defaultTerminalItem: TerminalTypeItem = {
    terminalId: CreateId(),
    terminalTypeId: "",
    selected: false,
    connectorType: ConnectorType.Input,
    number: 1,
    categoryId: defaultTerminal?.terminalCategoryId,
  };

  const handleTerminalClick = (terminal: TerminalType) => {
    setSearchbarInput(terminal.name);

    defaultTerminalItem.terminalTypeId = terminal.id;
    defaultTerminalItem.categoryId = terminal.terminalCategoryId;

    onTerminalCategoryChange("terminalTypeId", defaultTerminalItem);
    setExpandList(!expandList);
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearchbarInput(e.target.value.toLowerCase());
  };

  const isSelected = terminalTypes.some((t) => t.id === defaultTerminal?.id) ?? false;

  const showListItems = () => {
    const isInArray = terminalTypes.find((t) => t.name === searchbarInput);
    const filteredList = isInArray ? terminalTypes : filter;
    return filteredList.map((t) => {
      return (
        <SearchBarListItem
          key={t.id}
          onClick={() => {
            handleTerminalClick(t);
          }}
        >
          <p>{t.name}</p>
        </SearchBarListItem>
      );
    });
  };

  return (
    <TerminalListElement>
      <TerminalCategoryWrapper isSelected={isSelected}>
        <RadioButtonContainer
          id={categoryName}
          label={categoryName}
          listType={ListType.Terminals}
          checked={isSelected}
          defaultValue={terminalTypes[0].id}
          onChange={(key, terminalTypeId) => onPropertyChange(key, terminalTypeId)}
        />
        {isSelected && (
          <SearchBarContainer>
            <SearchBar>
              <label htmlFor="terminalsearch" />
              <input
                type="text"
                value={searchbarInput}
                placeholder={TextResources.TypeEditor_Search}
                onChange={handleChange}
                onFocus={() => setExpandList(!expandList)}
              />
              <img
                src={expandList ? ExpandIcon : CollapseIcon}
                alt="expand-icon"
                onClick={() => setExpandList(!expandList)}
                onFocus={() => setExpandList(!expandList)}
                className="icon"
              />
            </SearchBar>
            {expandList && <SearchBarList>{showListItems()}</SearchBarList>}
          </SearchBarContainer>
        )}
      </TerminalCategoryWrapper>
    </TerminalListElement>
  );
};

export default TransportInterfaceElement;
