import { useParams } from "react-router-dom";
import { useCreateUnit, useGetUnit, useUpdateUnit } from "../../../external/sources/unit/unit.queries";
import { FormMode } from "../types/formMode";

export const useUnitQuery = () => {
  const { id } = useParams();
  return useGetUnit(id);
};

export const useUnitMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateUnit();
  const updateMutation = useUpdateUnit(id);
  return mode === "edit" ? updateMutation : createMutation;
};
