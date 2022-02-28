import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeConditionLibAm } from "../../../models/typeLibrary/application/attributeConditionLibAm";
import { apiAttributeCondition } from "../../api/typeLibrary/apiAttributeCondition";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["attributeConditions"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetAttributeConditions = () => useQuery(keys.lists(), apiAttributeCondition.getAttributeConditions);

export const useCreateAttributeCondition = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeConditionLibAm) => apiAttributeCondition.postAttributeCondition(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateAttributeCondition = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: UpdateEntity<AttributeConditionLibAm>) => apiAttributeCondition.putAttributeCondition(item.id, item),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};
