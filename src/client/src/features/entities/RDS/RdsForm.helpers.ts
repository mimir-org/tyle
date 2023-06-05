import { useParams } from "react-router-dom";
import { useCreateRds, useGetRds, useUpdateRds } from "../../../external/sources/rds/rds.queries";
import { FormMode } from "../types/formMode";
import { RdsLibAm } from "@mimirorg/typelibrary-types";

export const useRdsQuery = () => {
  const { id } = useParams();
  return useGetRds(id);
};

export const useRdsMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateRds();
  const updateMutation = useUpdateRds(id);
  return mode === "edit" ? updateMutation : createMutation;
};

export const rdsCodeToUpper = (rds: RdsLibAm): RdsLibAm => ({
  ...rds,
  rdsCode: rds.rdsCode.toUpperCase(),
});
