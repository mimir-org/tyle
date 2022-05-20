import { useMutation, useQuery, useQueryClient } from "react-query";
import { NodeLibAm } from "../../../models/tyle/application/nodeLibAm";
import { apiNode } from "../../api/tyle/apiNode";

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

  return useMutation((unit: NodeLibAm) => apiNode.postLibraryNode(unit), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateNode = () => {
  const queryClient = useQueryClient();

  return useMutation((unit: NodeLibAm) => apiNode.putLibraryNode(unit), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.node(unit.id)),
  });
};

export const useDeleteNode = () => {
  const queryClient = useQueryClient();

  return useMutation((id: string) => apiNode.deleteLibraryNode(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
