import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { unitApi } from "external/sources/unit/unit.api";
import { State, UnitLibAm } from "@mimirorg/typelibrary-types";

const keys = {
  allUnits: ["units"] as const,
  unitLists: () => [...keys.allUnits, "list"] as const,
  unit: (id?: string) => [...keys.unitLists(), id] as const,
};

export const useGetUnits = () => useQuery(keys.unitLists(), unitApi.getUnits);

export const useGetUnit = (id?: string) =>
  useQuery(keys.unit(id), () => unitApi.getUnit(id), { enabled: !!id, retry: false });

export const useCreateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UnitLibAm) => unitApi.postUnit(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allUnits),
  });
};

export const useUpdateUnit = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: UnitLibAm) => unitApi.putUnit(item, id), {
    onSuccess: () => queryClient.invalidateQueries(keys.unit(id)),
  });
};

export const usePatchUnitState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => unitApi.patchUnitState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.unitLists()),
  });
};

export const useDeleteUnit = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => unitApi.deleteUnit(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.unitLists()),
  });
};
