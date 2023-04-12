import { ConnectorDirection, AspectObjectTerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";

export const getTerminalItemsFromFormData = (formTerminals: AspectObjectTerminalLibAm[], sourceTerminals?: TerminalLibCm[]) => {
  if (!sourceTerminals || sourceTerminals.length < 1) {
    return [];
  }

  const terminalItems: AspectObjectTerminalItem[] = [];

  formTerminals.forEach((formTerminal) => {
    const sourceTerminal = sourceTerminals.find((x) => x.id === formTerminal.terminalId);

    sourceTerminal &&
      terminalItems.push({
        name: sourceTerminal.name,
        color: sourceTerminal.color,
        maxQuantity: formTerminal.maxQuantity,
        direction: ConnectorDirection[formTerminal.connectorDirection] as keyof typeof ConnectorDirection,
      });
  });

  return terminalItems;
};
