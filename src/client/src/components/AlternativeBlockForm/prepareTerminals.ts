import { TerminalView } from "types/terminals/terminalView";

export const prepareTerminals = (terminals?: TerminalView[]) => {
  if (!terminals || terminals.length === 0) return [];

  return terminals.sort((a, b) => a.name.localeCompare(b.name));
};
