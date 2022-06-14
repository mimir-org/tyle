import { TerminalItem } from "../../content/home/types/TerminalItem";

export const sortTerminals = (terminals: TerminalItem[]) =>
  [...terminals].sort((a, b) => a.direction.localeCompare(b.direction) || a.name.localeCompare(b.name));
