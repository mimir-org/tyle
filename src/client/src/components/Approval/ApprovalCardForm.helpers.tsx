import { usePatchAttributeState } from "api/attribute.queries";
import { usePatchBlockState } from "api/block.queries";
import { usePatchTerminalState } from "api/terminal.queries";
import { AttributeView } from "types/attributes/attributeView";
import { BlockView } from "types/blocks/blockView";
import { TerminalView } from "types/terminals/terminalView";

export const usePatchStateMutation = (
  item: AttributeView | TerminalView | BlockView,
  itemType: "attribute" | "terminal" | "block",
) => {
  const patchAttributeStateMutation = usePatchAttributeState(item.id);
  const patchTerminalStateMutation = usePatchTerminalState(item.id);
  const patchBlockStateMutation = usePatchBlockState(item.id);

  switch (itemType) {
    case "attribute":
      return patchAttributeStateMutation;
    case "terminal":
      return patchTerminalStateMutation;
    case "block":
      return patchBlockStateMutation;
  }
};
