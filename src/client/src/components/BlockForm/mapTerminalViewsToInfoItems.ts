import { InfoItem } from "types/infoItem";
import { TerminalView } from "types/terminals/terminalView";

export const mapTerminalViewToInfoItem = (terminal: TerminalView): InfoItem => {
  const infoItem = {
    id: terminal.id,
    name: terminal.name,
    descriptors: {},
  };

  return infoItem;
};

export const mapTerminalViewsToInfoItems = (attributes: TerminalView[]): InfoItem[] =>
  attributes.map(mapTerminalViewToInfoItem);
