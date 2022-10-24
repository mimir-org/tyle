import { InterfaceLibCm, NodeLibCm, TerminalLibCm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "../../types/InterfaceItem";
import { NodeItem } from "../../types/NodeItem";
import { TerminalItem } from "../../types/TerminalItem";
import { TransportItem } from "../../types/TransportItem";

export type SearchResult = NodeItem | TerminalItem | TransportItem | InterfaceItem;

export type SearchResultRaw = NodeLibCm | TerminalLibCm | TransportLibCm | InterfaceLibCm;
