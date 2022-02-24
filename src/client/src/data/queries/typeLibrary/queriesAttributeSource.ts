import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeSourceLibAm } from "../../../models/typeLibrary/application/attributeSourceLibAm";
import { apiAttributeSource } from "../../api/typeLibrary/apiAttributeSource";

const keys = {
  all: ["attributeSources"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetAttributeSources = () => useQuery(keys.lists(), apiAttributeSource.getAttributeSources);

export const useCreateAttributeSource = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeSourceLibAm) => apiAttributeSource.postAttributeSource(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
