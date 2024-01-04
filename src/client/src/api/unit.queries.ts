import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { RdlUnitRequest } from "types/attributes/rdlUnitRequest";
import { unitApi } from "./unit.api";

const keys = {
  allUnits: ["units"] as const,
  unitLists: () => [...keys.allUnits, "list"] as const,
  unit: (id: number) => [...keys.unitLists(), id] as const,
};

export const useGetUnits = () => useQuery({ queryKey: keys.unitLists(), queryFn: unitApi.getUnits });

export const useGetUnit = (id: number) =>
  useQuery({ queryKey: keys.unit(id), queryFn: () => unitApi.getUnit(id), enabled: !!id, retry: false });

export const useCreateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: RdlUnitRequest) => unitApi.postUnit(item),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.allUnits }),
  });
};

export const useDeleteUnit = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => unitApi.deleteUnit(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.unitLists() }),
  });
};
