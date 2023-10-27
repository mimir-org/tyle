import { BlockItem } from "common/types/blockItem";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { BlockView } from "common/types/blocks/blockView";
import { State } from "common/types/common/state";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { sortInfoItems } from "../sorters";
import { mapAttributeViewsToInfoItems } from "./mapAttributeLibCmToInfoItem";
import { TerminalTypeReferenceView } from "common/types/blocks/terminalTypeReferenceView";

export const toBlockItem = (block: BlockView): BlockItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[block.state].label;

  return {
    id: block.id,
    name: block.name,
    img: block.symbol ?? "",
    description: block.description ?? "",
    color: getColorFromAspect(block.aspect),
    tokens: [block.version, currentStateLabel],
    terminals: sortBlockTerminals(mapBlockTerminalLibCmsToBlockTerminalItems(block.terminals)),
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(block.attributes.map(x => x.attribute))),
    kind: "BlockItem",
    state: block.state,
    createdBy: block.createdBy,
  };
};

const mapBlockTerminalLibCmsToBlockTerminalItems = (terminals: TerminalTypeReferenceView[]): BlockTerminalItem[] =>
  terminals.map((x) => ({
    id: x.terminal.id,
    name: x.terminal.name,
    color: getColorFromAspect(x.terminal.aspect),
    maxQuantity: x.maxCount,
    direction: x.direction,
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(x.terminal.attributes.map(x => x.attribute))),
  }));

const sortBlockTerminals = (terminals: BlockTerminalItem[]) =>
  [...terminals].sort((a, b) => a.direction.toString().localeCompare(b.direction.toString()) || a.name.localeCompare(b.name));
