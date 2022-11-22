import { InterfaceLibCm, State } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "common/types/interfaceItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { getValueLabelObjectsFromEnum } from "common/utils/getValueLabelObjectsFromEnum";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { mapTerminalLibCmToTerminalItem } from "common/utils/mappers/mapTerminalLibCmToTerminalItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapInterfaceLibCmToInterfaceItem = (interfaceLibCm: InterfaceLibCm): InterfaceItem => {
  const states = getValueLabelObjectsFromEnum(State);
  const currentStateLabel = states[interfaceLibCm.state].label;

  return {
    id: interfaceLibCm.id,
    name: interfaceLibCm.name,
    description: interfaceLibCm.description,
    aspectColor: getColorFromAspect(interfaceLibCm.aspect),
    interfaceColor: interfaceLibCm.terminal.color,
    attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(interfaceLibCm.attributes)),
    terminal: mapTerminalLibCmToTerminalItem(interfaceLibCm.terminal),
    tokens: [
      interfaceLibCm.version,
      interfaceLibCm.companyName,
      currentStateLabel,
      interfaceLibCm.rdsName,
      interfaceLibCm.purposeName,
    ],
    kind: "InterfaceItem",
  };
};
