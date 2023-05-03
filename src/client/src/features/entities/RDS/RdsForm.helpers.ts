import { useParams } from "react-router-dom";
import { TerminalFormMode } from "../terminal/types/terminalFormMode";
import { useCreateRds, useGetRds, useUpdateRds } from "../../../external/sources/rds/rds.queries";

export const useRdsQuery = () => {
  const { id } = useParams();
  return useGetRds(id);
};

export const useRdsMutation = (id?: string, mode?: TerminalFormMode) => {
  const createMutation = useCreateRds();
  const updateMutation = useUpdateRds(id);
  return mode === "edit" ? updateMutation : createMutation;
};
