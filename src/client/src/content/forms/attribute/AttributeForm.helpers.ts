import { Select } from "@mimirorg/typelibrary-types";
import { useParams } from "react-router-dom";
import { useCreateAttribute, useGetAttribute, useUpdateAttribute } from "../../../data/queries/tyle/queriesAttribute";
import { AttributeFormMode } from "./types/attributeFormMode";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const useAttributeMutation = (mode?: AttributeFormMode) => {
  const attributeUpdateMutation = useUpdateAttribute();
  const attributeCreateMutation = useCreateAttribute();
  return mode === "edit" ? attributeUpdateMutation : attributeCreateMutation;
};

export const showSelectValues = (attributeSelect: Select) => attributeSelect !== Select.None;
