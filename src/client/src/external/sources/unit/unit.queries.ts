import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { unitApi } from "external/sources/unit/unit.api";
import { UnitLibCm } from "@mimirorg/typelibrary-types";

const keys = {
  all: ["units"] as const,
  lists: () => [...keys.all, "list"] as const,
  unit: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetUnits = () => useQuery(keys.lists(), unitApi.getUnits);

export const useGetUnit = (id?: string) =>
  useQuery(keys.all, () => unitApi.getUnit(id), { enabled: !!id, retry: false });

export const useCreateUnit = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UnitLibCm) => unitApi.postUnit(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.all),
  });
};
