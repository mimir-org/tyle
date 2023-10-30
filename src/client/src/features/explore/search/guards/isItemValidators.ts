import { BlockView } from "common/types/blocks/blockView";
import { BlockItem } from "../../../../common/types/blockItem";
import { TerminalView } from "common/types/terminals/terminalView";
import { AttributeView } from "common/types/attributes/attributeView";

export const isBlockItem = (item: unknown): item is BlockItem => (<BlockItem>item).kind === "BlockItem";

export const isBlockView = (item: unknown): item is BlockView => (<BlockView>item).terminals !== undefined;

export const isTerminalView = (item: unknown): item is TerminalView => (<TerminalView>item).qualifier !== undefined;

export const isAttributeView = (item: unknown): item is AttributeView => (<AttributeView>item).units !== undefined;
