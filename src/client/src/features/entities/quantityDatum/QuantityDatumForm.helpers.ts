import { useParams } from "react-router-dom";
import {
  useCreateQuantityDatum,
  useGetQuantityDatum,
  useUpdateQuantityDatum,
} from "../../../external/sources/datum/quantityDatum.queries";
import { FormMode } from "../types/formMode";

export const useQuantityDatumQuery = () => {
  const { id } = useParams();
  return useGetQuantityDatum(id);
};

export const useQuantityDatumMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateQuantityDatum();
  const updateMutation = useUpdateQuantityDatum(id);
  return mode === "edit" ? updateMutation : createMutation;
};
