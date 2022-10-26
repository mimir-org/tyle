import { TerminalItem } from "../../../../../content/types/TerminalItem";

export const isTerminalItem = (item: unknown): item is TerminalItem => (<TerminalItem>item).kind === "TerminalItem";
