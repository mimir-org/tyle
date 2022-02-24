import { useMutation, useQuery, useQueryClient } from "react-query";
import { NodeLibAm } from "../../../models/typeLibrary/application/nodeLibAm";
import { apiAspectNode } from "../../api/typeLibrary/apiAspectNode";

const keys = {
  all: ["nodes"] as const,
  lists: () => [...keys.all, "list"] as const,
  node: (id: string) => [...keys.all, id] as const,
};

export const useGetAspectNodes = () => useQuery(keys.lists(), apiAspectNode.getAspectNodes);

export const useGetAspectNode = (id: string) => useQuery(keys.node(id), () => apiAspectNode.getAspectNode(id));

export const useCreateAspectNode = () => {
  const queryClient = useQueryClient();

  return useMutation((unit: NodeLibAm) => apiAspectNode.postAspectNode(unit), {
    onSuccess: () => {
      return queryClient.invalidateQueries(keys.lists());
    },
  });
};
