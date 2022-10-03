import { NodeLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiNode } from "../../api/tyle/apiNode";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["nodes"] as const,
  lists: () => [...keys.all, "list"] as const,
  node: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetNodes = () => useQuery(keys.lists(), apiNode.getLibraryNodes);

export const useGetNode = (id?: string) =>
  useQuery(keys.node(id), () => apiNode.getLibraryNode(id), { enabled: !!id, retry: false });

export const useCreateNode = () => {
  const queryClient = useQueryClient();

  return useMutation((item: NodeLibAm) => apiNode.postLibraryNode(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateNode = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UpdateEntity<NodeLibAm>) => apiNode.putLibraryNode(item.id, item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.node(unit.id)),
  });
};

export const usePatchNodeState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => apiNode.patchLibraryNodeState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
