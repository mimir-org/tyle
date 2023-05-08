import { AspectObjectLibCm, AttributeLibCm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { AspectObjectItem } from "common/types/aspectObjectItem";
import { TerminalItem } from "common/types/terminalItem";
import { AttributeItem } from "../../../../common/types/attributeItem";

export type SearchResult = AspectObjectItem | TerminalItem | AttributeItem;

export type SearchResultRaw = AspectObjectLibCm | TerminalLibCm | AttributeLibCm;
