import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { State } from "types/common/state";
import { StateChangeRequest } from "types/common/stateChangeRequest";
import { attributeApi } from "./attribute.api";
import { blockKeys } from "./block.queries";
import { terminalKeys } from "./terminal.queries";

const attributeKeys = {
  all: ["attributes"] as const,
  lists: () => [...attributeKeys.all, "list"] as const,
  list: (filters: string) => [...attributeKeys.lists(), { filters }] as const,
  details: () => [...attributeKeys.all, "detail"] as const,
  detail: (id?: string) => [...attributeKeys.details(), id] as const,
};

export const useGetAttributes = () =>
  useQuery({ queryKey: attributeKeys.list(""), queryFn: attributeApi.getAttributes });

export const useGetAttributesByState = (state: State) =>
  useQuery({ queryKey: attributeKeys.list(`state=${state}`), queryFn: () => attributeApi.getAttributesByState(state) });

export const useGetAttribute = (id?: string) =>
  useQuery({
    queryKey: attributeKeys.detail(id),
    queryFn: () => attributeApi.getAttribute(id),
    enabled: !!id,
    retry: false,
  });

export const useCreateAttribute = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: AttributeTypeRequest) => attributeApi.postAttribute(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: attributeKeys.list("") });
      queryClient.invalidateQueries({ queryKey: attributeKeys.list(`state=${State.Draft}`) });
    },
  });
};

export const useUpdateAttribute = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: AttributeTypeRequest) => attributeApi.putAttribute(id, item),
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: attributeKeys.list("") });
      queryClient.invalidateQueries({ queryKey: attributeKeys.list(`state=${data.state}`) });
      queryClient.invalidateQueries({ queryKey: attributeKeys.detail(id) });
    },
  });
};

export const usePatchAttributeState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: StateChangeRequest) => attributeApi.patchAttributeState(id, item),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: terminalKeys.all });
      queryClient.invalidateQueries({ queryKey: blockKeys.all });
      queryClient.invalidateQueries({ queryKey: attributeKeys.list("") });
      queryClient.invalidateQueries({ queryKey: attributeKeys.list(`state=${State.Review}`) });
      queryClient.invalidateQueries({
        queryKey: attributeKeys.list(`state=${variables.state === State.Approved ? State.Approved : State.Draft}`),
      });
      queryClient.invalidateQueries({ queryKey: attributeKeys.detail(id) });
    },
  });
};

export const useDeleteAttribute = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => attributeApi.deleteAttribute(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: attributeKeys.lists() }),
  });
};
