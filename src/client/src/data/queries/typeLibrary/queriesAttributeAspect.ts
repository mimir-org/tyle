import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeAspectLibAm } from "../../../models/typeLibrary/application/attributeAspectLibAm";
import { apiAttributeAspect } from "../../api/typeLibrary/apiAttributeAspect";

const keys = {
  all: ["attributeAspects"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetAttributeAspects = () => useQuery(keys.lists(), apiAttributeAspect.getAttributeAspects);

export const useCreateAttributeCondition = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeAspectLibAm) => apiAttributeAspect.postAttributeAspect(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
