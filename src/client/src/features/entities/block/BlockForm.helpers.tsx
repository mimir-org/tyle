import { Aspect, BlockTerminalLibCm } from "@mimirorg/typelibrary-types";
import { useCreateBlock, useGetBlock, useUpdateBlock } from "external/sources/block/block.queries";
import { BlockFormPredefinedAttributes } from "features/entities/block/predefined-attributes/BlockFormPredefinedAttributes";
import { BlockFormTerminals } from "features/entities/block/terminals/BlockFormTerminals";
import { useParams } from "react-router-dom";
import { FormMode } from "../types/formMode";
// import { UseFormResetField } from "react-hook-form";
import { BlockTypeRequest } from "common/types/blocks/blockTypeRequest";
import { BlockView } from "common/types/blocks/blockView";
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

export interface BlockFormFields extends BlockTypeRequest {}

/**
 * Resets the part of block form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
// export const resetSubform = (resetField: UseFormResetField<BlockFormFields>, newAspect: Aspect | undefined) => {
//   resetField("selectedAttributePredefined", { defaultValue: [] });
//   if (newAspect !== Aspect.Function && newAspect !== Aspect.Product) {
//     resetField("blockTerminals", { defaultValue: [] });
//   }
// };

export const getSubformForAspect = (aspect: Aspect, limitedTerminals?: BlockTerminalLibCm[]) => {
  switch (aspect) {
    case Aspect.Function:
      return <BlockFormTerminals limitedTerminals={limitedTerminals} />;
    case Aspect.Product:
      return <BlockFormTerminals limitedTerminals={limitedTerminals} />;
    case Aspect.Location:
      return <BlockFormPredefinedAttributes aspects={[aspect]} />;
    default:
      return <></>;
  }
};

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formBlock client-only model
 */
export const toApiModel = (formBlock: BlockFormFields): BlockTypeRequest => formBlock;

export const toClientModel = (block: BlockView): BlockFormFields => ({
  ...block,
  classifierIds: block.classifiers.map((x) => x.id),
  terminals: block.terminals,
});

export const createEmptyFormBlockLib = (): BlockFormFields => ({
  ...emptyBlockLibAm,
  attributes: [],
});

const emptyBlockLibAm: BlockTypeRequest = {
  name: "",
  rdsId: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  attributes: [],
  blockTerminals: [],
  selectedAttributePredefined: [],
  description: "",
  symbol: "",
  typeReference: "",
  version: "1.0",
  attributeGroups: [],
};

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
