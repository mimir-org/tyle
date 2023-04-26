import { useParams } from "react-router-dom";
import {
  useCreateAttribute,
  useGetAttribute,
  useUpdateAttribute,
} from "../../../external/sources/attribute/attribute.queries";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const useAttributeMutation = (id?: string, create?: boolean) => {
  const createMutation = useCreateAttribute();
  const updateMutation = useUpdateAttribute(id);
  return create ? createMutation : updateMutation;
};
