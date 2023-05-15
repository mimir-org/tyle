import { useMutation, useQuery, useQueryClient, UseQueryOptions } from "@tanstack/react-query";
import { QuantityDatumLibAm, State } from "@mimirorg/typelibrary-types";
import { quantityDatum } from "./quantityDatum";

const keys = {
  all: ["quantityDatums"] as const,
  lists: () => [...keys.all, "list"] as const,
  quantityDatum: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetQuantityDatums = (options?: Pick<UseQueryOptions, "staleTime">) =>
  useQuery(keys.lists(), quantityDatum.getQuantityDatums, options);

export const useGetQuantityQuantityDatum = (id?: string) =>
  useQuery(keys.quantityDatum(id), () => quantityDatum.getQuantityDatum(id), { enabled: !!id, retry: false });

export const useCreateQuantityDatum = () => {
  const queryClient = useQueryClient();

  return useMutation((item: QuantityDatumLibAm) => quantityDatum.postQuantityDatum(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateQuantityDatum = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: QuantityDatumLibAm) => quantityDatum.putQuantityDatum(item, id), {
    onSuccess: () => queryClient.invalidateQueries(keys.quantityDatum(id)),
  });
};

export const usePatchQuantityDatumState = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: { id: string; state: State }) => quantityDatum.patchQuantityDatumState(item.id, item.state),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};
