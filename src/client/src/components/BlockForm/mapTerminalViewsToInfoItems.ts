import { InfoItem } from "common/types/infoItem";

import { TerminalView } from "common/types/terminals/terminalView";

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
