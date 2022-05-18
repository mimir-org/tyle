import { UnitLibAm } from "../../../models/tyle/application/unitLibAm";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiUnit } from "../../api/tyle/apiUnit";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["units"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetUnits = () => useQuery(keys.lists(), apiUnit.getUnits);

export const useCreateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation((unit: UnitLibAm) => apiUnit.postUnit(unit), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation((unit: UpdateEntity<UnitLibAm>) => apiUnit.putUnit(unit.id, unit), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
