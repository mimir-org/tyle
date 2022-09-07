import { TransportLibCm } from "@mimirorg/typelibrary-types";
import { TransportItem } from "../../content/types/TransportItem";
import { sortInfoItems } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";

export const mapTransportLibCmToTransportItem = (transport: TransportLibCm): TransportItem => ({
  id: transport.id,
  name: transport.name,
  description: transport.description,
  color: transport.terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(transport.attributes)),
  tokens: [transport.createdBy, transport.version],
  kind: "TransportItem",
});
