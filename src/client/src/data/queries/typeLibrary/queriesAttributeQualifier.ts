import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeQualifierLibAm } from "../../../models/typeLibrary/application/attributeQualifierLibAm";
import { apiAttributeQualifier } from "../../api/typeLibrary/apiAttributeQualifier";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["attributeQualifiers"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetAttributeQualifiers = () => useQuery(keys.lists(), apiAttributeQualifier.getAttributeQualifiers);

export const useCreateAttributeQualifier = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeQualifierLibAm) => apiAttributeQualifier.postAttributeQualifier(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateAttributeQualifier = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: UpdateEntity<AttributeQualifierLibAm>) => apiAttributeQualifier.putAttributeQualifier(item.id, item),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};
