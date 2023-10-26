import { useMutation, useQuery, useQueryClient, UseQueryOptions } from "@tanstack/react-query";
import { State } from "common/types/common/state";
import { StateChangeRequest } from "common/types/common/stateChangeRequest";
import { TerminalTypeRequest } from "common/types/terminals/terminalTypeRequest";
import { terminalApi } from "external/sources/terminal/terminal.api";

const terminalKeys = {
  all: ["terminals"] as const,
  lists: () => [...terminalKeys.all, "list"] as const,
  detail: (id: string) => [...terminalKeys.all, "detail", id] as const,
};

export const useGetTerminals = () => useQuery(terminalKeys.lists(), terminalApi.getTerminals);

export const useGetTerminalsByState = (state: State) => useQuery(terminalKeys.lists(), () => terminalApi.getTerminalsByState(state));

export const useGetTerminal = (id: string) =>
  useQuery(terminalKeys.detail(id), () => terminalApi.getTerminal(id), { retry: false });

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalTypeRequest) => terminalApi.postTerminal(item), {
    onSuccess: () => queryClient.invalidateQueries(terminalKeys.lists()),
  });
};

export const useUpdateTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalTypeRequest) => terminalApi.putTerminal(id, item), {
    onSuccess: () => {
      queryClient.invalidateQueries(terminalKeys.lists());
      queryClient.invalidateQueries(terminalKeys.detail(id));
    }
  });
};

export const usePatchTerminalState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: StateChangeRequest) => terminalApi.patchTerminalState(id, item), {
    onSuccess: () => {
      queryClient.invalidateQueries(terminalKeys.lists());
      queryClient.invalidateQueries(terminalKeys.detail(id));
    },
  });
};

export const useDeleteTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => terminalApi.deleteTerminal(id), {
    onSuccess: () => queryClient.invalidateQueries(terminalKeys.lists()),
  });
};
