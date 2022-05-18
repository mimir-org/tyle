import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeSourceLibAm } from "../../../models/tyle/application/attributeSourceLibAm";
import { apiAttributeSource } from "../../api/tyle/apiAttributeSource";
import { UpdateEntity } from "../../types/updateEntity";

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

export const useUpdateAttributeSource = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: UpdateEntity<AttributeSourceLibAm>) => apiAttributeSource.putAttributeSource(item.id, item),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};
