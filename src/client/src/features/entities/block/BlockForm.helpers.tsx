import { Aspect, BlockTerminalLibCm } from "@mimirorg/typelibrary-types";
import { useCreateBlock, useGetBlock, useUpdateBlock } from "external/sources/block/block.queries";
import { BlockFormPredefinedAttributes } from "features/entities/block/predefined-attributes/BlockFormPredefinedAttributes";
import { BlockFormTerminals } from "features/entities/block/terminals/BlockFormTerminals";
import { FormBlockLib } from "features/entities/block/types/formBlockLib";
import { useParams } from "react-router-dom";
import { FormMode } from "../types/formMode";
import { UseFormResetField } from "react-hook-form";

export const useBlockQuery = () => {
  const { id } = useParams();
  return useGetBlock(id);
};

export const useBlockMutation = (id?: string, mode?: FormMode) => {
  const blockUpdateMutation = useUpdateBlock(id);
  const blockCreateMutation = useCreateBlock();
  return mode === "edit" ? blockUpdateMutation : blockCreateMutation;
};

/**
 * Resets the part of block form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: UseFormResetField<FormBlockLib>, newAspect: Aspect | undefined) => {
  resetField("selectedAttributePredefined", { defaultValue: [] });
  if (newAspect !== Aspect.Function && newAspect !== Aspect.Product) {
    resetField("blockTerminals", { defaultValue: [] });
  }
};

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
