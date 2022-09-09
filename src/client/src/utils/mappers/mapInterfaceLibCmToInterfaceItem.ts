import { InterfaceLibCm } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "../../content/types/InterfaceItem";
import { getColorFromAspect } from "../getColorFromAspect";
import { sortInfoItems } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";
import { mapTerminalLibCmToTerminalItem } from "./mapTerminalLibCmToTerminalItem";

export const mapInterfaceLibCmToInterfaceItem = (interfaceLibCm: InterfaceLibCm): InterfaceItem => ({
  id: interfaceLibCm.id,
  name: interfaceLibCm.name,
  description: interfaceLibCm.description,
  aspectColor: getColorFromAspect(interfaceLibCm.aspect),
  interfaceColor: interfaceLibCm.terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(interfaceLibCm.attributes)),
  terminal: mapTerminalLibCmToTerminalItem(interfaceLibCm.terminal),
  tokens: [interfaceLibCm.rdsName, interfaceLibCm.purposeName, interfaceLibCm.createdBy, interfaceLibCm.version],
  kind: "InterfaceItem",
});
