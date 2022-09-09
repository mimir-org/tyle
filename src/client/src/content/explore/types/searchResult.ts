import { AttributeLibCm, NodeLibCm, TerminalLibCm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../types/AttributeItem";
import { NodeItem } from "../../types/NodeItem";
import { TerminalItem } from "../../types/TerminalItem";
import { TransportItem } from "../../types/TransportItem";

export type SearchResult = NodeItem | AttributeItem | TerminalItem | TransportItem;

export type SearchResultRaw = NodeLibCm | AttributeLibCm | TerminalLibCm | TransportLibCm;
