import { UnitLibAm } from "../../../models/typeLibrary/application/unitLibAm";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiUnit } from "../../api/typeLibrary/apiUnit";

const keys = {
  all: ["units"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetUnits = () => useQuery(keys.lists(), apiUnit.getUnits);

export const useCreateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation((unit: UnitLibAm) => apiUnit.postUnit(unit), {
    onSuccess: () => {
      queryClient.invalidateQueries(keys.lists());
    },
  });
};
