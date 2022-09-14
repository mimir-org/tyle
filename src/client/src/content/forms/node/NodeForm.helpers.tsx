import { Aspect } from "@mimirorg/typelibrary-types";
import { TFunction } from "react-i18next";
import { useParams } from "react-router-dom";
import * as yup from "yup";
import { useCreateNode, useGetNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { NodeFormPredefinedAttributes } from "./predefined-attributes/NodeFormPredefinedAttributes";
import { NodeFormTerminalTable } from "./terminals/NodeFormTerminalTable";
import { FormNodeLib } from "./types/formNodeLib";

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

export const nodeSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup.string().max(60, t("node.validation.name.max")).required(t("node.validation.name.required")),
    rdsName: yup.string().required(t("node.validation.rdsName.required")),
    rdsCode: yup.string().required(t("node.validation.rdsCode.required")),
    purposeName: yup.string().required(t("node.validation.purposeName.required")),
    aspect: yup.number().required(t("node.validation.aspect.required")),
    companyId: yup.number().min(1, t("node.validation.companyId.min")).required(),
    description: yup.string().max(500, t("node.validation.description.max")),
    symbol: yup.string(),
    parentId: yup.string().nullable(),
    nodeTerminals: yup.array().of(
      yup.object().shape({
        terminalId: yup.string().required(t("node.validation.nodeTerminals.terminalId.required")),
      })
    ),
    attributeIdList: yup.array().of(
      yup.object().shape({
        value: yup.string().required(t("node.validation.attributeIdList.value.required")),
      })
    ),
    typeReferences: yup.array().of(
      yup.object().shape({
        name: yup.string().required(t("node.validation.typeReferences.name.required")),
      })
    ),
  });
