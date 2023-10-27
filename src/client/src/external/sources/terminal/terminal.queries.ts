import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { State } from "common/types/common/state";
import { StateChangeRequest } from "common/types/common/stateChangeRequest";
import { TerminalTypeRequest } from "common/types/terminals/terminalTypeRequest";
import { terminalApi } from "external/sources/terminal/terminal.api";

const terminalKeys = {
  all: ["terminals"] as const,
  lists: () => [...terminalKeys.all, "list"] as const,
  list: (filters: string) => [...terminalKeys.lists(), { filters }] as const,
  details: () => [...terminalKeys.all, "detail"] as const,
  detail: (id: string) => [...terminalKeys.details(), id] as const,
};

export const useGetTerminals = () => useQuery(terminalKeys.list(""), terminalApi.getTerminals);

export const useGetTerminalsByState = (state: State) =>
  useQuery(terminalKeys.list(`state=${state}`), () => terminalApi.getTerminalsByState(state));

export const useGetTerminal = (id: string) =>
  useQuery(terminalKeys.detail(id), () => terminalApi.getTerminal(id), { retry: false });

export const useCreateTerminal = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalTypeRequest) => terminalApi.postTerminal(item), {
    onSuccess: () => {
      queryClient.invalidateQueries(terminalKeys.list(""));
      queryClient.invalidateQueries(terminalKeys.list(`state=${State.Draft}`));
    },
  });
};

export const useUpdateTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: TerminalTypeRequest) => terminalApi.putTerminal(id, item), {
    onSuccess: (data) => {
      queryClient.invalidateQueries(terminalKeys.list(""));
      queryClient.invalidateQueries(terminalKeys.list(`state=${data.state}`));
      queryClient.invalidateQueries(terminalKeys.detail(id));
    },
  });
};

export const usePatchTerminalState = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: StateChangeRequest) => terminalApi.patchTerminalState(id, item), {
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries(terminalKeys.list(""));
      queryClient.invalidateQueries(terminalKeys.list(`state=${State.Review}`));
      queryClient.invalidateQueries(terminalKeys.list(`state=${variables.state === State.Approved ? State.Approved : State.Draft}`));
      queryClient.invalidateQueries(terminalKeys.detail(id));
    },
  });
};

export const useDeleteTerminal = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => terminalApi.deleteTerminal(id), {
    // TODO: Refine this?
    onSuccess: () => queryClient.invalidateQueries(terminalKeys.lists()),
  });
};
