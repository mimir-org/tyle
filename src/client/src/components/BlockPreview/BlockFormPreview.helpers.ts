import { BlockTerminalLibAm } from "@mimirorg/typelibrary-types";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { TerminalView } from "common/types/terminals/terminalView";
import { getColorFromAspect } from "common/utils/getColorFromAspect";

export const getTerminalItemsFromFormData = (formTerminals: BlockTerminalLibAm[], sourceTerminals?: TerminalView[]) => {
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
        color: getColorFromAspect(sourceTerminal.aspect),
        maxQuantity: formTerminal.maxQuantity,
        direction: sourceTerminal.qualifier,
      });
  });

  return terminalItems;
};
