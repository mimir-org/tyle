import { AttributeLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attributeApi } from "./attribute.api";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  attribute: (id?: string) => [...keys.attributeLists(), id] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), attributeApi.getAttributes);

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), attributeApi.getAttributesPredefined);

export const useGetAttribute = (id?: string) =>
  useQuery(keys.attribute(id), () => attributeApi.getAttribute(id), { enabled: !!id, retry: false });

export const useUpdateAttributes = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeLibAm) => attributeApi.putAttribute(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.attribute(unit.id)),
  });
};

export const usePatchAttributeState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => attributeApi.patchAttributeState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.attributeLists()),
  });
};

export const useCreateAttribute = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeLibAm) => attributeApi.postAttribute(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allAttributes),
  });
};

export const useUpdateAttribute = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeLibAm) => attributeApi.putAttribute(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.attribute(unit.id)),
  });
};
