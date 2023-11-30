import { InfoItem } from "types/infoItem";
import { Direction } from "types/terminals/direction";
import { TerminalView } from "types/terminals/terminalView";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";

export const resolveAvailableTerminals = (
  selectedTerminals: TerminalTypeReferenceField[],
  allTerminals: TerminalView[],
) => {
  const available: TerminalView[] = [];

  allTerminals.forEach((terminal) => {
    const numberOfEntries = selectedTerminals.filter(
      (selectedTerminalTypeReference) => selectedTerminalTypeReference.terminalId === terminal.id,
    ).length;

    if (
      (terminal.qualifier === Direction.Bidirectional && numberOfEntries < 3) ||
      (terminal.qualifier !== Direction.Bidirectional && numberOfEntries < 1)
    ) {
      available.push(terminal);
    }
  });

  return available;
};

export const onAddTerminals = (
  addedIds: string[],
  selectedTerminals: TerminalTypeReferenceField[],
  allTerminals: TerminalView[],
  setTerminals: (nextTerminals: TerminalTypeReferenceField[]) => void,
) => {
  let availableDirections = [Direction.Bidirectional, Direction.Input, Direction.Output];

  const terminalsToAdd: TerminalTypeReferenceField[] = [];

  addedIds.forEach((id) => {
    const targetTerminal = allTerminals.find((x) => x.id === id);

    if (targetTerminal === undefined) return;

    let defaultDirection: Direction;

    if (targetTerminal.qualifier !== Direction.Bidirectional) defaultDirection = targetTerminal.qualifier;
    else {
      selectedTerminals.forEach((x) => {
        if (x.terminalId === id) {
          availableDirections = availableDirections.filter((y) => y !== x.direction);
        }
      });

      defaultDirection = availableDirections[0];
    }

    terminalsToAdd.push({
      id: crypto.randomUUID(),
      terminalId: targetTerminal.id,
      terminalName: targetTerminal.name,
      terminalQualifier: targetTerminal.qualifier,
      minCount: 1,
      direction: defaultDirection,
      maxCount: null,
      connectionPoint: null,
    });
  });

  setTerminals([...selectedTerminals, ...terminalsToAdd]);
};

export const mapTerminalViewsToInfoItems = (terminals: TerminalView[]): InfoItem[] =>
  terminals.map((terminal) => ({
    id: terminal.id,
    name: terminal.name,
    descriptors: {},
  }));

export const prepareTerminals = (terminals?: TerminalView[]) => {
  if (!terminals || terminals.length === 0) return [];

  return terminals.sort((a, b) => a.name.localeCompare(b.name));
};
