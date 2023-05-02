import { AspectObjectLibCm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { AspectObjectItem } from "common/types/aspectObjectItem";
import { TerminalItem } from "common/types/terminalItem";

export type SearchResult = AspectObjectItem | TerminalItem;

export type SearchResultRaw = AspectObjectLibCm | TerminalLibCm;
