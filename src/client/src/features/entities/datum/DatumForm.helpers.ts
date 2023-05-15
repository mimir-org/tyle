import { useParams } from "react-router-dom";
import {
  useCreateQuantityDatum,
  useGetQuantityDatum,
  useUpdateQuantityDatum,
} from "../../../external/sources/datum/quantityDatum.queries";
import { TerminalFormMode } from "../terminal/types/terminalFormMode";

export const useDatumQuery = () => {
  const { id } = useParams();
  return useGetQuantityDatum(id);
};

export const useDatumMutation = (id?: string, mode?: TerminalFormMode) => {
  const createMutation = useCreateQuantityDatum();
  const updateMutation = useUpdateQuantityDatum(id);
  return mode === "edit" ? updateMutation : createMutation;
};
