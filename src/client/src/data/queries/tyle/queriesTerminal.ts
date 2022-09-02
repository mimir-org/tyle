import { TerminalLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiTerminal } from "../../api/tyle/apiTerminal";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
  terminal: (id?: string) => [...keys.lists(), id] as const,
  allReferences: ["terminalReferences"] as const,
  referenceLists: () => [...keys.allReferences, "list"] as const,
};

export const useGetTerminals = () => useQuery(keys.lists(), apiTerminal.getTerminals);

export const useGetTerminal = (id?: string) =>
  useQuery(keys.terminal(id), () => apiTerminal.getTerminal(id), { enabled: !!id, retry: false });

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalLibAm) => apiTerminal.postTerminal(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useGetTerminalReferences = () => useQuery(keys.referenceLists(), apiTerminal.getTerminalReferences);
