import { NodeTerminalLibAm } from "../../../models/tyle/application/nodeTerminalLibAm";
import { TerminalLibCm } from "../../../models/tyle/client/terminalLibCm";
import { Aspect } from "../../../models/tyle/enums/aspect";
import { ConnectorDirection } from "../../../models/tyle/enums/connectorDirection";
import { TerminalItem } from "../../home/types/TerminalItem";

export const getTerminalItemsFromFormData = (formTerminals: NodeTerminalLibAm[], sourceTerminals?: TerminalLibCm[]) => {
  if (!sourceTerminals || sourceTerminals.length < 1) {
    return [];
  }

  const terminalItems: TerminalItem[] = [];

  formTerminals.forEach((formTerminal) => {
    const sourceTerminal = sourceTerminals.find((x) => x.id === formTerminal.terminalId);

    sourceTerminal &&
      terminalItems.push({
        name: sourceTerminal.name,
        color: sourceTerminal.color,
        amount: formTerminal.number,
        direction: ConnectorDirection[formTerminal.connectorDirection] as keyof typeof ConnectorDirection,
      });
  });

  return terminalItems;
};

export const aspectOptions = [
  { value: Aspect.None, label: "None" },
  { value: Aspect.NotSet, label: "NotSet" },
  { value: Aspect.Location, label: "Location" },
  { value: Aspect.Function, label: "Function" },
  { value: Aspect.Product, label: "Product" },
];
