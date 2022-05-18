import { useMutation, useQuery, useQueryClient } from "react-query";
import { NodeLibAm } from "../../../models/tyle/application/nodeLibAm";
import { apiNode } from "../../api/tyle/apiNode";

const keys = {
  all: ["nodes"] as const,
  lists: () => [...keys.all, "list"] as const,
  node: (id?: string) => [...keys.all, id] as const,
};

export const useGetNodes = () => useQuery(keys.lists(), apiNode.getAspectNodes);

export const useGetNode = (id?: string) => useQuery(keys.node(id), () => apiNode.getAspectNode(id), { enabled: !!id });

export const useCreateNode = () => {
  const queryClient = useQueryClient();

  return useMutation((unit: NodeLibAm) => apiNode.postAspectNode(unit), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
