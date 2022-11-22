import { State, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { TerminalItem } from "common/types/terminalItem";
import { getValueLabelObjectsFromEnum } from "common/utils/getValueLabelObjectsFromEnum";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapTerminalLibCmToTerminalItem = (terminal: TerminalLibCm): TerminalItem => {
  const states = getValueLabelObjectsFromEnum(State);
  const currentStateLabel = states[terminal.state].label;

  return {
    id: terminal.id,
    name: terminal.name,
    description: terminal.description,
    color: terminal.color,
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(terminal.attributes)),
    tokens: [terminal.version, terminal.companyName, currentStateLabel],
    kind: "TerminalItem",
  };
};
