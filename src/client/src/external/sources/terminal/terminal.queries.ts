import { State, TerminalLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient, UseQueryOptions } from "@tanstack/react-query";
import { terminalApi } from "external/sources/terminal/terminal.api";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
  terminal: (id?: number) => [...keys.lists(), id] as const,
};

export const useGetTerminals = (options?: Pick<UseQueryOptions, "staleTime">) =>
  useQuery(keys.lists(), terminalApi.getTerminals, options);

export const useGetTerminal = (id?: number) =>
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

  return useMutation((item: { id: number; state: State }) => terminalApi.patchTerminalState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const usePatchTerminalStateReject = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: number }) => terminalApi.patchhTerminalStateReject(item.id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
