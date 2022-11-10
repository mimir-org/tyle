import { ConnectorDirection, NodeTerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";

export const getTerminalItemsFromFormData = (formTerminals: NodeTerminalLibAm[], sourceTerminals?: TerminalLibCm[]) => {
  if (!sourceTerminals || sourceTerminals.length < 1) {
    return [];
  }

  const terminalItems: NodeTerminalItem[] = [];

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
