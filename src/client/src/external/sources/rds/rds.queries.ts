import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { rdsApi } from "external/sources/rds/rds.api";
import { RdsLibAm, State } from "@mimirorg/typelibrary-types";

const keys = {
  all: ["rds"] as const,
  lists: () => [...keys.all, "list"] as const,
  rds: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetAllRds = () => useQuery(keys.lists(), rdsApi.getAllRds);

export const useGetRds = (id?: string) =>
  useQuery(keys.rds(id), () => rdsApi.getRds(id), { enabled: !!id, retry: false });

export const useCreateRds = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdsLibAm) => rdsApi.postRds(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateRds = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: RdsLibAm) => rdsApi.putRds(item, id), {
    onSuccess: () => queryClient.invalidateQueries(keys.rds(id)),
  });
};

export const usePatchRdsState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => rdsApi.patchRdsState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useDeleteRds = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => rdsApi.deleteRds(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
