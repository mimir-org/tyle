import { Aspect, NodeLibAm } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions, UnpackNestedValue } from "react-hook-form";
import { useParams } from "react-router-dom";
import textResources from "../../../assets/text/TextResources";
import { toast } from "../../../complib/data-display";
import { useGetNode } from "../../../data/queries/tyle/queriesNode";
import { UpdateEntity } from "../../../data/types/updateEntity";
import { FormNodeLib, mapFormNodeLibToApiModel, mapNodeLibCmToFormNodeLib } from "../types/formNodeLib";

export const aspectOptions = [
  { value: Aspect.None, label: "None" },
  { value: Aspect.NotSet, label: "NotSet" },
  { value: Aspect.Location, label: "Location" },
  { value: Aspect.Function, label: "Function" },
  { value: Aspect.Product, label: "Product" },
];

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledNodeData = (
  reset: (
    values?: DefaultValues<FormNodeLib> | UnpackNestedValue<FormNodeLib>,
    keepStateOptions?: KeepStateOptions
  ) => void
) => {
  const { id } = useParams();
  const nodeQuery = useGetNode(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && nodeQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapNodeLibCmToFormNodeLib(nodeQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, nodeQuery.isSuccess, nodeQuery.data, reset]);

  return hasPrefilled;
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

export const submitNodeData = (formData: FormNodeLib, mutate: (data: UpdateEntity<NodeLibAm>) => Promise<unknown>) => {
  const submittable = mapFormNodeLibToApiModel(formData);
  const submissionPromise = mutate(submittable);

  toast.promise(submissionPromise, {
    loading: textResources.FORMS_NODE_SUBMITTING,
    success: textResources.FORMS_NODE_SUBMITTING_SUCCESS,
    error: textResources.FORMS_NODE_SUBMITTING_ERROR,
  });
};
