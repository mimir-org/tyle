import { TerminalItem } from "../../../../common/types/terminalItem";

export const isTerminalItem = (item: unknown): item is TerminalItem => (<TerminalItem>item).kind === "TerminalItem";
