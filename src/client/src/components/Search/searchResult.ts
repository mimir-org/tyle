import { ItemType } from "../../common/types/itemTypes";
import { BlockView } from "common/types/blocks/blockView";
import { TerminalView } from "common/types/terminals/terminalView";
import { AttributeView } from "common/types/attributes/attributeView";
import { AttributeGroupView } from "common/types/attributes/attributeGroupView";

export type SearchResult = ItemType;

export type SearchResultRaw = BlockView | TerminalView | AttributeView | AttributeGroupView;
