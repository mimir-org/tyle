import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { BlockTypeRequest } from "types/blocks/blockTypeRequest";
import { State } from "types/common/state";
import { StateChangeRequest } from "types/common/stateChangeRequest";
import { blockApi } from "./block.api";

export const blockKeys = {
  all: ["blocks"] as const,
  lists: () => [...blockKeys.all, "list"] as const,
  list: (filters: string) => [...blockKeys.lists(), { filters }] as const,
  details: () => [...blockKeys.all, "detail"] as const,
  detail: (id?: string) => [...blockKeys.details(), id] as const,
};

export const useGetBlocks = () => useQuery({ queryKey: blockKeys.list(""), queryFn: blockApi.getBlocks });

export const useGetBlocksByState = (state: State) =>
  useQuery({ queryKey: blockKeys.list(`state=${state}`), queryFn: () => blockApi.getBlocksByState(state) });

export const useGetBlock = (id?: string) =>
  useQuery({ queryKey: blockKeys.detail(id), queryFn: () => blockApi.getBlock(id), enabled: !!id, retry: false });

export const useCreateBlock = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: BlockTypeRequest) => blockApi.postBlock(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: blockKeys.list("") });
      queryClient.invalidateQueries({ queryKey: blockKeys.list(`state=${State.Draft}`) });
    },
  });
};

export const useUpdateBlock = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: BlockTypeRequest) => blockApi.putBlock(id, item),
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: blockKeys.list("") });
      queryClient.invalidateQueries({ queryKey: blockKeys.list(`state=${data.state}`) });
      queryClient.invalidateQueries({ queryKey: blockKeys.detail(id) });
    },
  });
};

export const usePatchBlockState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: StateChangeRequest) => blockApi.patchBlockState(id, item),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: blockKeys.list("") });
      queryClient.invalidateQueries({ queryKey: blockKeys.list(`state=${State.Review}`) });
      queryClient.invalidateQueries({
        queryKey: blockKeys.list(`state=${variables.state === State.Approved ? State.Approved : State.Draft}`),
      });
      queryClient.invalidateQueries({ queryKey: blockKeys.detail(id) });
    },
  });
};

export const useDeleteBlock = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => blockApi.deleteBlock(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: blockKeys.lists() }),
  });
};
