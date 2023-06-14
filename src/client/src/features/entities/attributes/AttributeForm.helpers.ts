import { useParams } from "react-router-dom";
import {
  useCreateAttribute,
  useGetAttribute,
  useUpdateAttribute,
} from "../../../external/sources/attribute/attribute.queries";
import { FormMode } from "../types/formMode";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const useAttributeMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateAttribute();
  const updateMutation = useUpdateAttribute(id);
  return mode === "edit" ? updateMutation : createMutation;
};
