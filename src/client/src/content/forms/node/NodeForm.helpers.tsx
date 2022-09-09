import { Aspect } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { Control, DefaultValues, KeepStateOptions, UseFormRegister } from "react-hook-form";
import { useParams } from "react-router-dom";
import { useGetNode } from "../../../data/queries/tyle/queriesNode";
import { FormNodeLib, mapNodeLibCmToFormNodeLib } from "./types/formNodeLib";
import { FunctionNode, LocationNode, ProductNode } from "./variants";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledNodeData = (
  reset: (values?: DefaultValues<FormNodeLib> | FormNodeLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const nodeQuery = useGetNode(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && nodeQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapNodeLibCmToFormNodeLib(nodeQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, nodeQuery.isSuccess, nodeQuery.data, reset]);

  return [hasPrefilled, nodeQuery.isLoading];
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
