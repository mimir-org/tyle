import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "external/sources/terminal/terminal.queries";
import { useParams } from "react-router-dom";
import { TerminalFormMode } from "./types/terminalFormMode";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (mode?: TerminalFormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal();
  return mode === "edit" ? updateMutation : createMutation;
};
