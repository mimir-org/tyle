import { Aspect } from "@mimirorg/typelibrary-types";
import { useParams } from "react-router-dom";
import { useCreateNode, useGetNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { NodeFormPredefinedAttributes } from "./predefined-attributes/NodeFormPredefinedAttributes";
import { NodeFormTerminalTable } from "./terminals/NodeFormTerminalTable";
import { FormNodeLib } from "./types/formNodeLib";
import { NodeFormMode } from "./types/nodeFormMode";

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
  resetField("attributeIdList");
};

export const getSubformForAspect = (aspect: Aspect) => {
  switch (aspect) {
    case Aspect.Function:
      return <NodeFormTerminalTable />;
    case Aspect.Product:
      return <NodeFormTerminalTable />;
    case Aspect.Location:
      return <NodeFormPredefinedAttributes aspects={[aspect]} />;
    default:
      return <></>;
  }
};
