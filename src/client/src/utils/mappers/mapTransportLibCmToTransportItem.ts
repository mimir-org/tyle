import { TransportLibCm } from "@mimirorg/typelibrary-types";
import { TransportItem } from "../../content/types/TransportItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { sortInfoItems } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";
import { mapTerminalLibCmToTerminalItem } from "./mapTerminalLibCmToTerminalItem";

export const mapTransportLibCmToTransportItem = (transport: TransportLibCm): TransportItem => ({
  id: transport.id,
  name: transport.name,
  description: transport.description,
  aspectColor: getColorFromAspect(transport.aspect),
  transportColor: transport.terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(transport.attributes)),
  terminal: mapTerminalLibCmToTerminalItem(transport.terminal),
  tokens: [transport.rdsName, transport.purposeName, transport.createdBy, transport.version],
  kind: "TransportItem",
});
