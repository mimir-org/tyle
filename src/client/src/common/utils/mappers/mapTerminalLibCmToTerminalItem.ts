import { State, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { TerminalItem } from "common/types/terminalItem";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapTerminalLibCmToTerminalItem = (terminal: TerminalLibCm): TerminalItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[terminal.state].label;

  return {
    id: terminal.id,
    name: terminal.name,
    description: terminal.description,
    color: terminal.color,
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(terminal.attributes)),
    tokens: [currentStateLabel],
    kind: "TerminalItem",
    state: terminal.state,
    createdBy: terminal.createdBy,
  };
};
