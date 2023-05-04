import { useMutation, useQuery, useQueryClient, UseQueryOptions } from "@tanstack/react-query";
import { QuantityDatumLibAm } from "@mimirorg/typelibrary-types";
import { datumApi } from "./datum.api";

const keys = {
  all: ["datums"] as const,
  lists: () => [...keys.all, "list"] as const,
  datum: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetDatums = (options?: Pick<UseQueryOptions, "staleTime">) =>
  useQuery(keys.lists(), datumApi.getDatums, options);

export const useGetDatum = (id?: string) =>
  useQuery(keys.datum(id), () => datumApi.getDatum(id), { enabled: !!id, retry: false });

export const useCreateDatum = () => {
  const queryClient = useQueryClient();

  return useMutation((item: QuantityDatumLibAm) => datumApi.postDatum(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateDatum = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: QuantityDatumLibAm) => datumApi.putDatum(item, id), {
    onSuccess: () => queryClient.invalidateQueries(keys.datum(id)),
  });
};
