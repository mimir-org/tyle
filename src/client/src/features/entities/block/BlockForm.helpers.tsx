// import { Aspect, BlockTerminalLibCm } from "@mimirorg/typelibrary-types";
import { useCreateBlock, useGetBlock, useUpdateBlock } from "external/sources/block/block.queries";
// import { BlockFormPredefinedAttributes } from "features/entities/block/predefined-attributes/BlockFormPredefinedAttributes";
// import { BlockFormTerminals } from "features/entities/block/terminals/BlockFormTerminals";
import { useParams } from "react-router-dom";
import { FormMode } from "../types/formMode";
// import { UseFormResetField } from "react-hook-form";
import { BlockTypeRequest } from "common/types/blocks/blockTypeRequest";
import { BlockView } from "common/types/blocks/blockView";
//import { RdlClassifier } from "common/types/common/rdlClassifier";
import { TerminalTypeReferenceView } from "common/types/blocks/terminalTypeReferenceView";
import { AttributeTypeReferenceView } from "common/types/common/attributeTypeReferenceView";

import { RdlPurpose } from "common/types/common/rdlPurpose";
import { RdlClassifier } from "common/types/common/rdlClassifier";
import { InfoItem } from "common/types/infoItem";
import { TerminalView } from "common/types/terminals/terminalView";
import { mapTerminalViewsToInfoItems } from "common/utils/mappers/mapTerminalViewsToInfoItems";
import { Direction } from "common/types/terminals/direction";
// import { Direction } from "common/types/terminals/direction";
// import { ValueObject } from "features/entities/types/valueObject";

export const useBlockQuery = () => {
  const { id } = useParams();
  return useGetBlock(id);
};

export const useBlockMutation = (id?: string, mode?: FormMode) => {
  const blockUpdateMutation = useUpdateBlock(id);
  const blockCreateMutation = useCreateBlock();
  return mode === "edit" ? blockUpdateMutation : blockCreateMutation;
};

export interface BlockFormFields
  extends Omit<BlockTypeRequest, "purposeId" | "terminals" | "attributes" | "classifierIds" | "terminal"> {
  purpose?: RdlPurpose;
  terminals: TerminalTypeReferenceView[];
  attributes: AttributeTypeReferenceView[];
  classifiers: RdlClassifier[];
  terminal?: TerminalTypeReferenceView;
}

export const toBlockFormFields = (block: BlockView): BlockFormFields => ({
  ...block,
});

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formBlock client-only model
 */
// export const toApiModel = (formBlock: BlockFormFields): BlockTypeRequest => formBlock;

// export const toClientModel = (block: BlockView): BlockFormFields => ({
//   ...block,
//   classifierIds: block.classifiers.map((x) => x.id),
//   terminals: block.terminals,
// });
export const toBlockTypeRequest = (blockFormFields: BlockFormFields): BlockTypeRequest => ({
  name: blockFormFields.name,
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

// const emptyBlockLibAm: BlockTypeRequest = {
//   name: "",
//   rdsId: "",
//   purposeName: "",
//   aspect: Aspect.None,
//   companyId: 0,
//   attributes: [],
//   blockTerminals: [],
//   selectedAttributePredefined: [],
//   description: "",
//   symbol: "",
//   typeReference: "",
//   version: "1.0",
//   attributeGroups: [],
// };

// export interface FormAttributePredefinedLib extends Omit<SelectedAttributePredefinedLibAm, "values"> {
//   values: ValueObject<string>[];
// }

// /**
//  * Maps the client-only model back to the model expected by the backend api
//  * @param formSelectedAttribute client-only model
//  */
// export const mapFormAttributePredefinedLibToApiModel = (
//   formSelectedAttribute: FormAttributePredefinedLib,
// ): SelectedAttributePredefinedLibAm => {
//   const predefinedAttributesMap: { [key: string]: boolean } = {};
//   formSelectedAttribute.values?.forEach((x) => (predefinedAttributesMap[x.value] = true));

//   return {
//     ...formSelectedAttribute,
//     values: predefinedAttributesMap,
//   };
// };

// export const mapAttributePredefinedLibCmToClientModel = (
//   attribute: SelectedAttributePredefinedLibCm,
// ): FormAttributePredefinedLib => ({
//   ...attribute,
//   values: Object.keys(attribute.values).map((y) => ({ value: y })),
// });

// /**
//  * This type functions as a layer between client needs and the backend model.
//  * It allows you to adapt the expected api model to fit client/form logic needs.
//  *
//  * hasMaxQuantity - used to allow a more friendly way for the user toggle between an infinite and a concrete amount of terminals.
//  */
// export interface FormBlockTerminalLib extends BlockTerminalLibAm {
//   hasMaxQuantity: boolean;
// }

// export const mapBlockTerminalLibCmToClientModel = (blockTerminalLibCm: BlockTerminalLibCm): FormBlockTerminalLib => ({
//   ...mapBlockTerminalLibCmToBlockTerminalLibAm(blockTerminalLibCm),
//   hasMaxQuantity:
//     blockTerminalLibCm.maxQuantity > MINIMUM_TERMINAL_QUANTITY_VALUE &&
//     blockTerminalLibCm.maxQuantity < MAXIMUM_TERMINAL_QUANTITY_VALUE,
// });

// const mapBlockTerminalLibCmToBlockTerminalLibAm = (terminal: BlockTerminalLibCm): BlockTerminalLibAm => ({
//   ...terminal,
//   terminalId: terminal.terminal.id,
// });

// export const createEmptyFormBlockTerminalLib = (): FormBlockTerminalLib => ({
//   ...emptyBlockTerminalLibAm,
//   hasMaxQuantity: false,
// });

// const emptyBlockTerminalLibAm: BlockTerminalLibAm = {
//   terminalId: "",
//   minQuantity: 0,
//   maxQuantity: 0,
//   connectorDirection: ConnectorDirection.Input,
// };

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
