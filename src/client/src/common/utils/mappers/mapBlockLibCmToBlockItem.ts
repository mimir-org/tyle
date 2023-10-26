import { ConnectorDirection, BlockLibCm, BlockTerminalLibCm, State } from "@mimirorg/typelibrary-types";
import { BlockItem } from "common/types/blockItem";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
//import { mapAttributeViewsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
//import { sortInfoItems } from "common/utils/sorters";

export const mapBlockLibCmToBlockItem = (block: BlockLibCm): BlockItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[block.state].label;

  return {
    id: block.id,
    name: block.name,
    img: block.symbol,
    description: block.description,
    color: getColorFromAspect(block.aspect),
    tokens: [block.version, block.companyName, currentStateLabel, block.rdsName, block.purposeName],
    terminals: sortBlockTerminals(mapBlockTerminalLibCmsToBlockTerminalItems(block.blockTerminals)),
    attributes: [], //sortInfoItems(mapAttributeViewsToInfoItems(block.attributes)),
    kind: "BlockItem",
    state: block.state,
    companyId: block.companyId,
    createdBy: block.createdBy,
  };
};

const mapBlockTerminalLibCmsToBlockTerminalItems = (terminals: BlockTerminalLibCm[]): BlockTerminalItem[] =>
  terminals.map((x) => ({
    id: x.terminal.id,
    name: x.terminal.name,
    color: x.terminal.color,
    maxQuantity: x.maxQuantity,
    direction: ConnectorDirection[x.connectorDirection] as keyof typeof ConnectorDirection,
    attributes: [], //sortInfoItems(mapAttributeViewsToInfoItems(x.terminal.attributes)),
  }));

const sortBlockTerminals = (terminals: BlockTerminalItem[]) =>
  [...terminals].sort((a, b) => a.direction.localeCompare(b.direction) || a.name.localeCompare(b.name));
