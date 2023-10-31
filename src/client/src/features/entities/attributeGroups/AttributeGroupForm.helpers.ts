import { useParams } from "react-router-dom";
import {
  useCreateAttributeGroup,
  useGetAttributeGroup,
  useUpdateAttributeGroup,
} from "../../../api/attributeGroup.queries";
import { FormMode } from "../types/formMode";

export const useAttributeGroupQuery = () => {
  const { id } = useParams();
  return useGetAttributeGroup(id ?? "");
};

export const useAttributeGroupMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateAttributeGroup();
  const updateMutation = useUpdateAttributeGroup(id ?? "");
  return mode === "edit" ? updateMutation : createMutation;
};
