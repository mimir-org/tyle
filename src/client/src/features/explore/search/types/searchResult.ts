import { InterfaceLibCm, NodeLibCm, TerminalLibCm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "common/types/interfaceItem";
import { NodeItem } from "common/types/nodeItem";
import { TerminalItem } from "common/types/terminalItem";
import { TransportItem } from "common/types/transportItem";

export type SearchResult = NodeItem | TerminalItem | TransportItem | InterfaceItem;

export type SearchResultRaw = NodeLibCm | TerminalLibCm | TransportLibCm | InterfaceLibCm;
