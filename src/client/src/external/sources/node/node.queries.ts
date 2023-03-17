import { NodeLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { nodeApi } from "external/sources/node/node.api";

const keys = {
  all: ["nodes"] as const,
  lists: () => [...keys.all, "list"] as const,
  node: (id?: number) => [...keys.lists(), id] as const,
};

export const useGetNodes = () => useQuery(keys.lists(), nodeApi.getLibraryNodes);

export const useGetNode = (id?: number) =>
  useQuery(keys.node(id), () => nodeApi.getLibraryNode(id), { enabled: !!id, retry: false });

export const useCreateNode = () => {
  const queryClient = useQueryClient();

  return useMutation((item: NodeLibAm) => nodeApi.postLibraryNode(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateNode = (id?: number) => {
  const queryClient = useQueryClient();

  return useMutation((item: NodeLibAm) => nodeApi.putLibraryNode(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.node(unit.id)),
  });
};

export const usePatchNodeState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: number; state: State }) => nodeApi.patchLibraryNodeState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const usePatchNodeStateReject = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: number }) => nodeApi.patchLibraryNodeStateReject(item.id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
