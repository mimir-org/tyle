import { ConnectorDirection, BlockTerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { BlockTerminalItem } from "common/types/blockTerminalItem";

export const getTerminalItemsFromFormData = (
  formTerminals: BlockTerminalLibAm[],
  sourceTerminals?: TerminalLibCm[],
) => {
  if (!sourceTerminals || sourceTerminals.length < 1) {
    return [];
  }

  const terminalItems: BlockTerminalItem[] = [];

  formTerminals.forEach((formTerminal) => {
    const sourceTerminal = sourceTerminals.find((x) => x.id === formTerminal.terminalId);

    sourceTerminal &&
      terminalItems.push({
        id: sourceTerminal.id,
        name: sourceTerminal.name,
        color: sourceTerminal.color,
        maxQuantity: formTerminal.maxQuantity,
        direction: ConnectorDirection[formTerminal.connectorDirection] as keyof typeof ConnectorDirection,
      });
  });

  return terminalItems;
};
