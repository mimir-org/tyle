import { InterfaceLibCm } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "common/types/interfaceItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { mapTerminalLibCmToTerminalItem } from "common/utils/mappers/mapTerminalLibCmToTerminalItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapInterfaceLibCmToInterfaceItem = (interfaceLibCm: InterfaceLibCm): InterfaceItem => ({
  id: interfaceLibCm.id,
  name: interfaceLibCm.name,
  description: interfaceLibCm.description,
  aspectColor: getColorFromAspect(interfaceLibCm.aspect),
  interfaceColor: interfaceLibCm.terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(interfaceLibCm.attributes)),
  terminal: mapTerminalLibCmToTerminalItem(interfaceLibCm.terminal),
  tokens: [
    interfaceLibCm.rdsName,
    interfaceLibCm.purposeName,
    interfaceLibCm.createdBy,
    interfaceLibCm.version,
    interfaceLibCm.companyName,
  ],
  kind: "InterfaceItem",
});
