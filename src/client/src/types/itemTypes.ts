import { AttributeGroupItem } from "types/attributeGroupItem";
import { AttributeItem } from "./attributeItem";
import { BlockItem } from "./blockItem";
import { TerminalItem } from "./terminalItem";

export type ItemType = BlockItem | TerminalItem | AttributeItem | AttributeGroupItem;
