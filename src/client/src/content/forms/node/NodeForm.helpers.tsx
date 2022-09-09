import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, UseFormRegister } from "react-hook-form";
import { useParams } from "react-router-dom";
import { useCreateNode, useGetNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { FormNodeLib } from "./types/formNodeLib";
import { FunctionNode, LocationNode, ProductNode } from "./variants";

export const useNodeQuery = () => {
  const { id } = useParams();
  return useGetNode(id);
};

export const useNodeMutation = (isEdit?: boolean) => {
  const nodeUpdateMutation = useUpdateNode();
  const nodeCreateMutation = useCreateNode();
  return isEdit ? nodeUpdateMutation : nodeCreateMutation;
};

/**
 * Resets the part of node form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormNodeLib) => void) => {
  resetField("selectedAttributePredefined");
  resetField("nodeTerminals");
  resetField("attributeIdList");
};

export const getFormForAspect = (
  aspect: Aspect,
  control: Control<FormNodeLib>,
  register: UseFormRegister<FormNodeLib>
) => {
  switch (aspect) {
    case Aspect.Function:
      return <FunctionNode control={control} />;
    case Aspect.Product:
      return <ProductNode control={control} />;
    case Aspect.Location:
      return <LocationNode control={control} register={register} />;
    default:
      return <></>;
  }
};
