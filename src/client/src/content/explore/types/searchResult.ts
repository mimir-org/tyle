import { AttributeLibCm, NodeLibCm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../types/AttributeItem";
import { NodeItem } from "../../types/NodeItem";
import { TerminalItem } from "../../types/TerminalItem";

export type SearchResult = NodeItem | AttributeItem | TerminalItem;

export type SearchResultRaw = NodeLibCm | AttributeLibCm | TerminalLibCm;
