import { ConnectorDirection, AspectObjectLibCm, AspectObjectTerminalLibCm, State } from "@mimirorg/typelibrary-types";
import { AspectObjectItem } from "common/types/aspectObjectItem";
import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapAspectObjectLibCmToAspectObjectItem = (aspectObject: AspectObjectLibCm): AspectObjectItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[aspectObject.state].label;

  return {
    id: aspectObject.id,
    name: aspectObject.name,
    img: aspectObject.symbol,
    description: aspectObject.description,
    color: getColorFromAspect(aspectObject.aspect),
    tokens: [
      aspectObject.version,
      aspectObject.companyName,
      currentStateLabel,
      aspectObject.rdsName,
      aspectObject.purposeName,
    ],
    terminals: sortAspectObjectTerminals(
      mapAspectObjectTerminalLibCmsToAspectObjectTerminalItems(aspectObject.aspectObjectTerminals)
    ),
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(aspectObject.attributes)),
    kind: "AspectObjectItem",
    state: aspectObject.state,
    companyId: aspectObject.companyId,
  };
};

const mapAspectObjectTerminalLibCmsToAspectObjectTerminalItems = (
  terminals: AspectObjectTerminalLibCm[]
): AspectObjectTerminalItem[] =>
  terminals.map((x) => ({
    id: x.terminal.id,
    name: x.terminal.name,
    color: x.terminal.color,
    maxQuantity: x.maxQuantity,
    direction: ConnectorDirection[x.connectorDirection] as keyof typeof ConnectorDirection,
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(x.terminal.attributes)),
  }));

const sortAspectObjectTerminals = (terminals: AspectObjectTerminalItem[]) =>
  [...terminals].sort((a, b) => a.direction.localeCompare(b.direction) || a.name.localeCompare(b.name));
