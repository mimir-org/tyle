import { InterfaceLibCm, NodeLibCm, TerminalLibCm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { InterfaceItem } from "../../../content/types/InterfaceItem";
import { NodeItem } from "../../../content/types/NodeItem";
import { TerminalItem } from "../../../content/types/TerminalItem";
import { TransportItem } from "../../../content/types/TransportItem";

export type SearchResult = NodeItem | TerminalItem | TransportItem | InterfaceItem;

export type SearchResultRaw = NodeLibCm | TerminalLibCm | TransportLibCm | InterfaceLibCm;
