import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attributeApi } from "./attribute.api";
import { AttributeTypeRequest } from "common/types/attributes/attributeTypeRequest";
import { State } from "common/types/common/state";
import { StateChangeRequest } from "common/types/common/stateChangeRequest";

const attributeKeys = {
  all: ["attributes"] as const,
  lists: () => [...attributeKeys.all, "list"] as const,
  list: (filters: string) => [...attributeKeys.lists(), { filters }] as const,
  details: () => [...attributeKeys.all, "detail"] as const,
  detail: (id: string) => [...attributeKeys.details(), id] as const,
};

export const useGetAttributes = () => useQuery(attributeKeys.list(""), attributeApi.getAttributes);

export const useGetAttributesByState = (state: State) =>
  useQuery(attributeKeys.list(`state=${state}`), () => attributeApi.getAttributesByState(state));

export const useGetAttribute = (id: string) =>
  useQuery(attributeKeys.detail(id), () => attributeApi.getAttribute(id), { retry: false });

export const useCreateAttribute = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeTypeRequest) => attributeApi.postAttribute(item), {
    onSuccess: () => {
      queryClient.invalidateQueries(attributeKeys.list(""));
      queryClient.invalidateQueries(attributeKeys.list(`state=${State.Draft}`));
    },
  });
};

export const useUpdateAttribute = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeTypeRequest) => attributeApi.putAttribute(id, item), {
    onSuccess: (data) => {
      queryClient.invalidateQueries(attributeKeys.list(""));
      queryClient.invalidateQueries(attributeKeys.list(`state=${data.state}`));
      queryClient.invalidateQueries(attributeKeys.detail(id));
    },
  });
};

export const usePatchAttributeState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: StateChangeRequest) => attributeApi.patchAttributeState(id, item), {
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries(attributeKeys.list(""));
      queryClient.invalidateQueries(attributeKeys.list(`state=${State.Review}`));
      queryClient.invalidateQueries(
        attributeKeys.list(`state=${variables.state === State.Approved ? State.Approved : State.Draft}`),
      );
      queryClient.invalidateQueries(attributeKeys.detail(id));
    },
  });
};

export const useDeleteAttribute = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => attributeApi.deleteAttribute(id), {
    // TODO: Refine this?
    onSuccess: () => queryClient.invalidateQueries(attributeKeys.lists()),
  });
};
