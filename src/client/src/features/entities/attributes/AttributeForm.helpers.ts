import { useCreateTerminal, useUpdateTerminal } from "external/sources/terminal/terminal.queries";
import { useParams } from "react-router-dom";
import { useGetAttribute } from "../../../external/sources/attribute/attribute.queries";

export const useAttributeQuery = () => {
  const { id } = useParams();
  return useGetAttribute(id);
};

export const useAttributeMutation = (id?: string, create?: boolean) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal(id);
  return create ? createMutation : updateMutation;
};
