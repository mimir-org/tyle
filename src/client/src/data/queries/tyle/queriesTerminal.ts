import { useMutation, useQuery, useQueryClient } from "react-query";
import { TerminalLibAm } from "../../../models/tyle/application/terminalLibAm";
import { apiRds } from "../../api/tyle/apiTerminal";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetTerminals = () => useQuery(keys.lists(), apiRds.getTerminals);

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalLibAm) => apiRds.postTerminal(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
