import { NodeLibCm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { NodeItem } from "common/types/nodeItem";
import { TerminalItem } from "common/types/terminalItem";

export type SearchResult = NodeItem | TerminalItem;

export type SearchResultRaw = NodeLibCm | TerminalLibCm;
