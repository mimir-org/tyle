import { AttributeLibCm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { AttributeItem } from "../../types/AttributeItem";
import { NodeItem } from "../../types/NodeItem";

export type SearchResult = NodeItem | AttributeItem;

export type SearchResultRaw = NodeLibCm | AttributeLibCm;
