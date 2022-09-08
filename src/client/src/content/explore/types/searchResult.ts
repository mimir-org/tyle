import { AttributeLibCm, InterfaceLibCm, NodeLibCm, TerminalLibCm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../types/AttributeItem";
import { InterfaceItem } from "../../types/InterfaceItem";
import { NodeItem } from "../../types/NodeItem";
import { TerminalItem } from "../../types/TerminalItem";
import { TransportItem } from "../../types/TransportItem";

export type SearchResult = NodeItem | AttributeItem | TerminalItem | TransportItem | InterfaceItem;

export type SearchResultRaw = NodeLibCm | AttributeLibCm | TerminalLibCm | TransportLibCm | InterfaceLibCm;
