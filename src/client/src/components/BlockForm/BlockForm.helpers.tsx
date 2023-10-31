import { useCreateBlock, useGetBlock, useUpdateBlock } from "api/block.queries";
import { useParams } from "react-router-dom";
import { FormMode } from "../../common/types/formMode";
import { BlockTypeRequest } from "common/types/blocks/blockTypeRequest";
import { BlockView } from "common/types/blocks/blockView";
import { TerminalTypeReferenceView } from "common/types/blocks/terminalTypeReferenceView";
import { AttributeTypeReferenceView } from "common/types/common/attributeTypeReferenceView";
import { RdlPurpose } from "common/types/common/rdlPurpose";
import { RdlClassifier } from "common/types/common/rdlClassifier";
import { InfoItem } from "common/types/infoItem";
import { TerminalView } from "common/types/terminals/terminalView";
import { mapTerminalViewsToInfoItems } from "common/utils/mappers/mapTerminalViewsToInfoItems";
import { Direction } from "common/types/terminals/direction";

export const useBlockQuery = () => {
  const { id } = useParams();
  return useGetBlock(id ?? "");
};

export const useBlockMutation = (id?: string, mode?: FormMode) => {
  const blockUpdateMutation = useUpdateBlock(id ?? "");
  const blockCreateMutation = useCreateBlock();
  return mode === "edit" ? blockUpdateMutation : blockCreateMutation;
};

export interface BlockFormFields
  extends Omit<BlockTypeRequest, "purposeId" | "terminals" | "attributes" | "classifierIds"> {
  purpose?: RdlPurpose;
  terminals: TerminalTypeReferenceView[];
  attributes: AttributeTypeReferenceView[];
  classifiers: RdlClassifier[];
}

export const toBlockFormFields = (block: BlockView): BlockFormFields => ({
  ...block,
});

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formBlock client-only model
 */

export const toBlockTypeRequest = (blockFormFields: BlockFormFields): BlockTypeRequest => ({
  ...blockFormFields,
  purposeId: blockFormFields.purpose?.id,
  classifierIds: blockFormFields.classifiers.map((x) => x.id),
  terminals: blockFormFields.terminals.map((x) => ({ ...x, terminalId: x.terminal.id })),
  attributes: blockFormFields.attributes.map((x) => ({ ...x, attributeId: x.attribute.id })),
});

export const createDefaultBlockFormFields = (): BlockFormFields => ({
  name: "",
  classifiers: [],
  terminals: [],
  attributes: [],
});

export const terminalInfoItem = (terminal: TerminalView): InfoItem => ({
  name: terminal.name,
  descriptors: {
    Description: terminal.qualifier,
  },
  id: terminal.id,
});

export const resolveSelectedAndAvailableTerminals = (
  fieldTerminals: TerminalTypeReferenceView[],
  allTerminals: TerminalView[],
) => {
  const selectedSet = new Set<string>();
  fieldTerminals.forEach((x) => selectedSet.add(x.terminal.id));

  const selected: TerminalView[] = [];
  const available: TerminalView[] = [];
  allTerminals.forEach((x) => {
    if (selectedSet.has(x.id)) {
      selected.push(x);
    }

    const numberOfEntries = fieldTerminals.filter((y) => y.terminal.id === x.id).length;

    if (
      (x.qualifier === Direction.Bidirectional && numberOfEntries < 3) ||
      (x.qualifier !== Direction.Bidirectional && numberOfEntries < 1)
    ) {
      available.push(x);
    }
  });

  return [mapTerminalViewsToInfoItems(available), mapTerminalViewsToInfoItems(selected)];
};

export const onAddTerminals = (
  selectedIds: string[],
  allTerminals: TerminalView[],
  allSelectedTerminals: TerminalTypeReferenceView[],
  append: (item: TerminalTypeReferenceView) => void,
) => {
  let availableDirections = [Direction.Bidirectional, Direction.Input, Direction.Output];

  selectedIds.forEach((id) => {
    const targetTerminal = allTerminals.find((x) => x.id === id);

    if (targetTerminal === undefined) return;

    let defaultDirection: Direction;

    if (targetTerminal.qualifier !== Direction.Bidirectional) defaultDirection = targetTerminal.qualifier;
    else {
      allSelectedTerminals.forEach((x) => {
        if (x.terminal.id === id) {
          availableDirections = availableDirections.filter((y) => y !== x.direction);
        }
      });

      defaultDirection = availableDirections[0];
    }

    append({ terminal: targetTerminal, minCount: 1, direction: defaultDirection });
  });
};
