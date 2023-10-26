import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { unitApi } from "external/sources/unit/unit.api";
import { RdlUnitRequest } from "common/types/attributes/rdlUnitRequest";

const keys = {
  allUnits: ["units"] as const,
  unitLists: () => [...keys.allUnits, "list"] as const,
  unit: (id: number) => [...keys.unitLists(), id] as const,
};

export const useGetUnits = () => useQuery(keys.unitLists(), unitApi.getUnits);

export const useGetUnit = (id: number) =>
  useQuery(keys.unit(id), () => unitApi.getUnit(id), { enabled: !!id, retry: false });

export const useCreateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdlUnitRequest) => unitApi.postUnit(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allUnits),
  });
};

export const useDeleteUnit = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation(() => unitApi.deleteUnit(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.unitLists()),
  });
};
