import { NodeTerminalItem } from "../../content/types/NodeTerminalItem";

export const sortNodeTerminals = (terminals: NodeTerminalItem[]) =>
  [...terminals].sort((a, b) => a.direction.localeCompare(b.direction) || a.name.localeCompare(b.name));
