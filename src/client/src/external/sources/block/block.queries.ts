import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { BlockTypeRequest } from "common/types/blocks/blockTypeRequest";
import { blockApi } from "external/sources/block/block.api";

const keys = {
  all: ["blocks"] as const,
  lists: () => [...keys.all, "list"] as const,
  block: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetBlocks = () => useQuery(keys.lists(), blockApi.getBlocks);

export const useGetBlock = (id?: string) =>
  useQuery(keys.block(id), () => blockApi.getBlock(id), { enabled: !!id, retry: false });

export const useGetLatestApprovedBlock = (id?: string, enable = true) =>
  useQuery(keys.block("latest-approved/" + id), () => blockApi.getLatestApprovedBlock(id), {
    enabled: enable && !!id,
    retry: false,
  });

export const useCreateBlock = () => {
  const queryClient = useQueryClient();
  return useMutation((item: BlockTypeRequest) => blockApi.postBlock(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateBlock = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: BlockTypeRequest) => blockApi.putBlock(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.block(unit.id)),
  });
};

export const useDeleteBlock = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => blockApi.deleteBlock(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
