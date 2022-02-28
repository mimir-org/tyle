import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeFormatLibAm } from "../../../models/typeLibrary/application/attributeFormatLibAm";
import { apiAttributeFormat } from "../../api/typeLibrary/apiAttributeFormat";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["attributeFormats"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetAttributeFormats = () => useQuery(keys.lists(), apiAttributeFormat.getAttributeFormats);

export const useCreateAttributeFormat = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeFormatLibAm) => apiAttributeFormat.postAttributeFormat(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateAttributeFormat = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: UpdateEntity<AttributeFormatLibAm>) => apiAttributeFormat.putAttributeFormat(item.id, item),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};
