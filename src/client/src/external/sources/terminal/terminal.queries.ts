import { useMutation, useQuery, useQueryClient, UseQueryOptions } from "@tanstack/react-query";
import { TerminalTypeRequest } from "common/types/terminals/terminalTypeRequest";
import { terminalApi } from "external/sources/terminal/terminal.api";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
  terminal: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetTerminals = (options?: Pick<UseQueryOptions, "staleTime">) =>
  useQuery(keys.lists(), terminalApi.getTerminals, options);

export const useGetTerminal = (id: string) =>
  useQuery(keys.terminal(id), () => terminalApi.getTerminal(id), { enabled: !!id, retry: false });

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalTypeRequest) => terminalApi.postTerminal(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalTypeRequest) => terminalApi.putTerminal(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.terminal(unit.id)),
  });
};

export const useDeleteTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => terminalApi.deleteTerminal(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
