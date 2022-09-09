import { TerminalLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiTerminal } from "../../api/tyle/apiTerminal";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
  terminal: (id?: string) => [...keys.lists(), id] as const,
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

export const useUpdateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UpdateEntity<TerminalLibAm>) => apiTerminal.putTerminal(item.id, item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.terminal(unit.id)),
  });
};

export const useDeleteTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((id: string) => apiTerminal.deleteTerminal(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
