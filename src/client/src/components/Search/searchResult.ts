import { AttributeGroupView } from "types/attributes/attributeGroupView";
import { AttributeView } from "types/attributes/attributeView";
import { BlockView } from "types/blocks/blockView";
import { ItemType } from "types/itemTypes";
import { TerminalView } from "types/terminals/terminalView";

export type SearchResult = ItemType;

export type SearchResultRaw = BlockView | TerminalView | AttributeView | AttributeGroupView;
