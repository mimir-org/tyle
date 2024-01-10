import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { State } from "types/common/state";
import { StateChangeRequest } from "types/common/stateChangeRequest";
import { TerminalTypeRequest } from "types/terminals/terminalTypeRequest";
import { blockKeys } from "./block.queries";
import { terminalApi } from "./terminal.api";

export const terminalKeys = {
  all: ["terminals"] as const,
  lists: () => [...terminalKeys.all, "list"] as const,
  list: (filters: string) => [...terminalKeys.lists(), { filters }] as const,
  details: () => [...terminalKeys.all, "detail"] as const,
  detail: (id?: string) => [...terminalKeys.details(), id] as const,
};

export const useGetTerminals = () => useQuery({ queryKey: terminalKeys.list(""), queryFn: terminalApi.getTerminals });

export const useGetTerminalsByState = (state: State) =>
  useQuery({ queryKey: terminalKeys.list(`state=${state}`), queryFn: () => terminalApi.getTerminalsByState(state) });

export const useGetTerminal = (id?: string) =>
  useQuery({
    queryKey: terminalKeys.detail(id),
    queryFn: () => terminalApi.getTerminal(id),
    enabled: !!id,
    retry: false,
  });

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: TerminalTypeRequest) => terminalApi.postTerminal(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: terminalKeys.list("") });
      queryClient.invalidateQueries({ queryKey: terminalKeys.list(`state=${State.Draft}`) });
    },
  });
};

export const useUpdateTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: TerminalTypeRequest) => terminalApi.putTerminal(id, item),
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: terminalKeys.list("") });
      queryClient.invalidateQueries({ queryKey: terminalKeys.list(`state=${data.state}`) });
      queryClient.invalidateQueries({ queryKey: terminalKeys.detail(id) });
    },
  });
};

export const usePatchTerminalState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: StateChangeRequest) => terminalApi.patchTerminalState(id, item),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: blockKeys.all });
      queryClient.invalidateQueries({ queryKey: terminalKeys.list("") });
      queryClient.invalidateQueries({ queryKey: terminalKeys.list(`state=${State.Review}`) });
      queryClient.invalidateQueries({
        queryKey: terminalKeys.list(`state=${variables.state === State.Approved ? State.Approved : State.Draft}`),
      });
      queryClient.invalidateQueries({ queryKey: terminalKeys.detail(id) });
    },
  });
};

export const useDeleteTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => terminalApi.deleteTerminal(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: terminalKeys.lists() }),
  });
};
