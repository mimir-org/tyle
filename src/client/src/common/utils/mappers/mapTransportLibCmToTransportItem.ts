import { TransportLibCm } from "@mimirorg/typelibrary-types";
import { TransportItem } from "common/types/transportItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { mapTerminalLibCmToTerminalItem } from "common/utils/mappers/mapTerminalLibCmToTerminalItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapTransportLibCmToTransportItem = (transport: TransportLibCm): TransportItem => ({
  id: transport.id,
  name: transport.name,
  description: transport.description,
  aspectColor: getColorFromAspect(transport.aspect),
  transportColor: transport.terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(transport.attributes)),
  terminal: mapTerminalLibCmToTerminalItem(transport.terminal),
  tokens: [transport.version, transport.companyName, transport.rdsName, transport.purposeName],
  kind: "TransportItem",
});
