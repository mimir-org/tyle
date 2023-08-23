import { BlockLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
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

  return useMutation((item: BlockLibAm) => blockApi.postBlock(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateBlock = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: BlockLibAm) => blockApi.putBlock(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.block(unit.id)),
  });
};

export const usePatchBlockState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => blockApi.patchBlockState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useDeleteBlock = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => blockApi.deleteBlock(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
