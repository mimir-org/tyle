import { State } from "common/types/common/state";
import { TerminalItem } from "common/types/terminalItem";
import { TerminalView } from "common/types/terminals/terminalView";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { getColorFromAspect } from "../getColorFromAspect";
import { mapAttributeViewsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const toTerminalItem = (terminal: TerminalView): TerminalItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[terminal.state].label;

  return {
    id: terminal.id,
    name: terminal.name,
    description: terminal.description ?? "",
    color: getColorFromAspect(terminal.aspect),
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(terminal.attributes.map((x) => x.attribute))),
    tokens: [currentStateLabel],
    kind: "TerminalItem",
    state: terminal.state,
    createdBy: terminal.createdBy,
  };
};
