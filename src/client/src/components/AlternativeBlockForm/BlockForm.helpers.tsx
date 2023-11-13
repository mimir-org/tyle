import { useCreateBlock, useGetBlock, useUpdateBlock } from "api/block.queries";
import { useParams } from "react-router-dom";
import { BlockTypeRequest } from "types/blocks/blockTypeRequest";
import { BlockView } from "types/blocks/blockView";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { TerminalTypeReferenceView } from "types/blocks/terminalTypeReferenceView";
import { Aspect } from "types/common/aspect";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { RdlClassifier } from "types/common/rdlClassifier";
import { RdlPurpose } from "types/common/rdlPurpose";
import { FormMode } from "types/formMode";
import { InfoItem } from "types/infoItem";
import { Direction } from "types/terminals/direction";
import { TerminalView } from "types/terminals/terminalView";
import { mapTerminalViewsToInfoItems } from "./mapTerminalViewsToInfoItems";

export const useBlockQuery = () => {
  const { id } = useParams();
  return useGetBlock(id);
};

export const useBlockMutation = (id?: string, mode?: FormMode) => {
  const blockUpdateMutation = useUpdateBlock(id ?? "");
  const blockCreateMutation = useCreateBlock();
  return mode === "edit" ? blockUpdateMutation : blockCreateMutation;
};

export interface BlockFormFields {
  name: string;
  description: string;
  classifiers: RdlClassifier[];
  purpose: RdlPurpose | null;
  notation: string;
  symbol: EngineeringSymbol | null;
  aspect: Aspect | null;
  terminals: TerminalTypeReferenceView[];
  attributes: AttributeTypeReferenceView[];
}

export const createEmptyBlockFormFields = (): BlockFormFields => ({
  name: "",
  description: "",
  classifiers: [],
  purpose: null,
  notation: "",
  symbol: null,
  aspect: null,
  terminals: [],
  attributes: [],
});

export const toBlockFormFields = (blockView: BlockView): BlockFormFields => ({
  ...blockView,
  description: blockView.description ?? "",
  purpose: blockView.purpose ?? null,
  notation: blockView.notation ?? "",
  symbol: blockView.symbol ?? null,
  aspect: blockView.aspect ?? null,
});

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formBlock client-only model
 */

export const toBlockTypeRequest = (blockFormFields: BlockFormFields): BlockTypeRequest => ({
  ...blockFormFields,
  description: blockFormFields.description ? blockFormFields.description : null,
  classifierIds: blockFormFields.classifiers.map((classifier) => classifier.id),
  purposeId: blockFormFields.purpose?.id ?? null,
  notation: blockFormFields.notation ? blockFormFields.notation : null,
  symbolId: blockFormFields.symbol?.id ?? null,
  terminals: blockFormFields.terminals.map((terminalTypeReferenceView) => ({
    ...terminalTypeReferenceView,
    terminalId: terminalTypeReferenceView.terminal.id,
  })),
  attributes: blockFormFields.attributes.map((attributeTypeReferenceView) => ({
    ...attributeTypeReferenceView,
    attributeId: attributeTypeReferenceView.attribute.id,
  })),
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
  append: (item: TerminalTypeReferenceView) => void,
  allSelectedTerminals?: TerminalTypeReferenceView[],
) => {
  let availableDirections = [Direction.Bidirectional, Direction.Input, Direction.Output];

  selectedIds.forEach((id) => {
    const targetTerminal = allTerminals.find((x) => x.id === id);

    if (targetTerminal === undefined) return;

    let defaultDirection: Direction;

    if (targetTerminal.qualifier !== Direction.Bidirectional) defaultDirection = targetTerminal.qualifier;
    else {
      (allSelectedTerminals ?? []).forEach((x) => {
        if (x.terminal.id === id) {
          availableDirections = availableDirections.filter((y) => y !== x.direction);
        }
      });

      defaultDirection = availableDirections[0];
    }

    append({ terminal: targetTerminal, minCount: 1, direction: defaultDirection });
  });
};
