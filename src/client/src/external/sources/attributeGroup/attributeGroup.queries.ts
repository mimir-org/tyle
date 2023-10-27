import { AttributeGroupLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attributeGroupApi } from "./attributeGroup.api";

const attributeGroupKeys = {
  all: ["attributeGroups"] as const,
  lists: () => [...attributeGroupKeys.all, "list"] as const,
  list: (filters: string) => [...attributeGroupKeys.lists(), { filters }] as const,
  details: () => [...attributeGroupKeys.all, "detail"] as const,
  detail: (id: string) => [...attributeGroupKeys.details(), id] as const,
};

export const useGetAttributeGroups = () => useQuery(attributeGroupKeys.list(""), attributeGroupApi.getAttributeGroups);

export const useGetAttributeGroup = (id: string) =>
  useQuery(attributeGroupKeys.detail(id), () => attributeGroupApi.getAttributeGroup(id), { retry: false });

export const useCreateAttributeGroup = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeGroupLibAm) => attributeGroupApi.postAttributeGroup(item), {
    onSuccess: () => queryClient.invalidateQueries(attributeGroupKeys.list("")),
  });
};

export const useUpdateAttributeGroup = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeGroupLibAm) => attributeGroupApi.putAttributeGroup(id, item), {
    onSuccess: () => {
      queryClient.invalidateQueries(attributeGroupKeys.list(""));
      queryClient.invalidateQueries(attributeGroupKeys.detail(id));
    },
  });
};

export const useDeleteAttributeGroup = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => attributeGroupApi.deleteAttributeGroup(id), {
    onSuccess: () => {
      queryClient.invalidateQueries(attributeGroupKeys.list(""));
      queryClient.invalidateQueries(attributeGroupKeys.detail(id));
    },
  });
};
