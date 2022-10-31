import { State, TerminalLibAm } from "@mimirorg/typelibrary-types";
import { terminalApi } from "external/sources/terminal/terminal.api";
import { useMutation, useQuery, useQueryClient } from "react-query";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
  terminal: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetTerminals = () => useQuery(keys.lists(), terminalApi.getTerminals);

export const useGetTerminal = (id?: string) =>
  useQuery(keys.terminal(id), () => terminalApi.getTerminal(id), { enabled: !!id, retry: false });

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalLibAm) => terminalApi.postTerminal(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalLibAm) => terminalApi.putTerminal(item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.terminal(unit.id)),
  });
};

export const usePatchTerminalState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => terminalApi.patchTerminalState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
