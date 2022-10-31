import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "external/sources/terminal/terminal.queries";
import { TerminalFormMode } from "features/entities/terminal/types/terminalFormMode";
import { useParams } from "react-router-dom";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (mode?: TerminalFormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal();
  return mode === "edit" ? updateMutation : createMutation;
};
