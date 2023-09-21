import { AttributeGroupLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attributeGroupApi } from "./attributeGroup.api";

const keys = {
  allAttributes: ["attributeGroup"] as const,
  attributeGroupList: () => [...keys.allAttributes, "list"] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  attributeGroup: (id?: string) => [...keys.attributeGroupList(), id] as const,
};

export const useGetAttributeGroups = () => useQuery(keys.attributeGroupList(), attributeGroupApi.getAttributeGroups);

export const useGetAttributeGroup = (id?: string) =>
  useQuery(keys.attributeGroup(id), () => attributeGroupApi.getAttributeGroup(id), { enabled: !!id, retry: false });

export const useCreateAttributeGroup = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeGroupLibAm) => attributeGroupApi.postAttributeGroup(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allAttributes),
  });
};

export const useUpdateAttributeGroup = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeGroupLibAm) => attributeGroupApi.putAttributeGroup(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.attributeGroup(unit.id)),
  });
};

// export const usePatchAttributeState = () => {
//   const queryClient = useQueryClient();

//   return useMutation((item: { id: string; state: State }) => attributeApi.patchAttributeState(item.id, item.state), {
//     onSuccess: () => queryClient.invalidateQueries(keys.attributeLists()),
//   });
// };

export const useDeleteAttributeGroup = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => attributeGroupApi.deleteAttributeGroup(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.attributeGroupList()),
  });
};
