import { useParams } from "react-router-dom";
import {
  useCreateQuantityDatum,
  useGetQuantityDatum,
  useUpdateQuantityDatum,
} from "../../../external/sources/datum/quantityDatum.queries";
import { TerminalFormMode } from "../terminal/types/terminalFormMode";

export const useQuantityDatumQuery = () => {
  const { id } = useParams();
  return useGetQuantityDatum(id);
};

export const useQuantityDatumMutation = (id?: string, mode?: TerminalFormMode) => {
  const createMutation = useCreateQuantityDatum();
  const updateMutation = useUpdateQuantityDatum(id);
  return mode === "edit" ? updateMutation : createMutation;
};
