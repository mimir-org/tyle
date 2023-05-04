import { useParams } from "react-router-dom";
import { useCreateDatum, useGetDatum, useUpdateDatum } from "../../../external/sources/datum/datum.queries";
import { TerminalFormMode } from "../terminal/types/terminalFormMode";

export const useDatumQuery = () => {
  const { id } = useParams();
  return useGetDatum(id);
};

export const useDatumMutation = (id?: string, mode?: TerminalFormMode) => {
  const createMutation = useCreateDatum();
  const updateMutation = useUpdateDatum(id);
  return mode === "edit" ? updateMutation : createMutation;
};
