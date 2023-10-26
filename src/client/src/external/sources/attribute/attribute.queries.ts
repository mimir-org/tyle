import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attributeApi } from "./attribute.api";
import { AttributeTypeRequest } from "common/types/attributes/attributeTypeRequest";
import { State } from "common/types/common/state";
import { StateChangeRequest } from "common/types/common/stateChangeRequest";

export const attributeKeys = {
  all: ["attributes"] as const,
  lists: () => [...attributeKeys.all, "list"] as const,
  detail: (id: string) => [...attributeKeys.all, "detail", id] as const,
};

export const useGetAttributes = () => useQuery(attributeKeys.lists(), attributeApi.getAttributes);

export const useGetAttributesByState = (state: State) => useQuery(attributeKeys.lists(), () => attributeApi.getAttributesByState(state));

export const useGetAttribute = (id: string) =>
  useQuery(attributeKeys.detail(id), () => attributeApi.getAttribute(id), { retry: false });

export const useCreateAttribute = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeTypeRequest) => attributeApi.postAttribute(item), {
    onSuccess: () => queryClient.invalidateQueries(attributeKeys.lists()),
  });
};

export const useUpdateAttribute = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeTypeRequest) => attributeApi.putAttribute(item, id), {
    onSuccess: () => {
      queryClient.invalidateQueries(attributeKeys.lists());
      queryClient.invalidateQueries(attributeKeys.detail(id));
    }
  });
};

export const usePatchAttributeState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: StateChangeRequest) => attributeApi.patchAttributeState(id, item), {
    onSuccess: () => {
      queryClient.invalidateQueries(attributeKeys.lists());
      queryClient.invalidateQueries(attributeKeys.detail(id));
    },
  });
};

export const useDeleteAttribute = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => attributeApi.deleteAttribute(id), {
    onSuccess: () => queryClient.invalidateQueries(attributeKeys.lists()),
  });
};
