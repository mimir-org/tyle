import { Aspect } from "@mimirorg/typelibrary-types";
import { useCreateNode, useGetNode, useUpdateNode } from "external/sources/node/node.queries";
import { NodeFormPredefinedAttributes } from "features/entities/node/predefined-attributes/NodeFormPredefinedAttributes";
import { NodeFormTerminals } from "features/entities/node/terminals/NodeFormTerminals";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";
import { NodeFormMode } from "features/entities/node/types/nodeFormMode";
import { useParams } from "react-router-dom";

export const useNodeQuery = () => {
  const { id } = useParams();
  return useGetNode(id);
};

export const useNodeMutation = (mode?: NodeFormMode) => {
  const nodeUpdateMutation = useUpdateNode();
  const nodeCreateMutation = useCreateNode();
  return mode === "edit" ? nodeUpdateMutation : nodeCreateMutation;
};

/**
 * Resets the part of node form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormNodeLib) => void) => {
  resetField("selectedAttributePredefined");
  resetField("nodeTerminals");
  resetField("attributes");
};

export const getSubformForAspect = (aspect: Aspect, mode?: NodeFormMode) => {
  switch (aspect) {
    case Aspect.Function:
      return <NodeFormTerminals canRemoveTerminals={mode !== "edit"} />;
    case Aspect.Product:
      return <NodeFormTerminals canRemoveTerminals={mode !== "edit"} />;
    case Aspect.Location:
      return <NodeFormPredefinedAttributes aspects={[aspect]} />;
    default:
      return <></>;
  }
};
