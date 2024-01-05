import { AttributeView } from "types/attributes/attributeView";
import { BlockView } from "types/blocks/blockView";
import { TerminalView } from "types/terminals/terminalView";

export const isBlockView = (item: unknown): item is BlockView => (<BlockView>item).terminals !== undefined;

export const isTerminalView = (item: unknown): item is TerminalView => (<TerminalView>item).qualifier !== undefined;

export const isAttributeView = (item: unknown): item is AttributeView => (<AttributeView>item).units !== undefined;
