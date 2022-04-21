import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeAspectLibAm } from "../../../models/typeLibrary/application/attributeAspectLibAm";
import { apiAttributeAspect } from "../../api/typeLibrary/apiAttributeAspect";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["attributeAspects"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetAttributeAspects = () => useQuery(keys.lists(), apiAttributeAspect.getAttributeAspects);

export const useCreateAttributeAspect = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeAspectLibAm) => apiAttributeAspect.postAttributeAspect(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateAttributeAspect = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: UpdateEntity<AttributeAspectLibAm>) => apiAttributeAspect.putAttributeAspect(item.id, item),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};
