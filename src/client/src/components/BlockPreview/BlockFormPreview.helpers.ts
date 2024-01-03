import { getColorFromAspect } from "helpers/aspect.helper";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { TerminalTypeReferenceRequest } from "types/blocks/terminalTypeReferenceRequest";
import { TerminalView } from "types/terminals/terminalView";

export const getTerminalItemsFromFormData = (
  formTerminals: TerminalTypeReferenceRequest[],
  sourceTerminals?: TerminalView[],
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
        color: getColorFromAspect(sourceTerminal.aspect ?? null),
        maxQuantity: formTerminal.maxCount ?? undefined,
        direction: sourceTerminal.qualifier,
      });
  });

  return terminalItems;
};
