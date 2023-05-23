import { useParams } from "react-router-dom";
import { useCreateRds, useGetRds, useUpdateRds } from "../../../external/sources/rds/rds.queries";
import { FormMode } from "../types/formMode";

export const useRdsQuery = () => {
  const { id } = useParams();
  return useGetRds(id);
};

export const useRdsMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateRds();
  const updateMutation = useUpdateRds(id);
  return mode === "edit" ? updateMutation : createMutation;
};
