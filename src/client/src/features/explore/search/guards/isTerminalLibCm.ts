import { TerminalLibCm } from "@mimirorg/typelibrary-types";

export const isTerminalLibCm = (item: unknown): item is TerminalLibCm => (<TerminalLibCm>item).kind === "TerminalLibCm";
