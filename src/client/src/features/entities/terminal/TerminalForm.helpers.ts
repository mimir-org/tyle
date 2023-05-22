import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "external/sources/terminal/terminal.queries";
import { useParams } from "react-router-dom";
import { FormMode } from "../types/formMode";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal(id);
  return mode === "edit" ? updateMutation : createMutation;
};
