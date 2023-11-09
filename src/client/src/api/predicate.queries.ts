import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { RdlPredicateRequest } from "types/attributes/rdlPredicateRequest";
import { predicateApi } from "./predicate.api";

const keys = {
  allPredicates: ["predicates"] as const,
  predicateLists: () => [...keys.allPredicates, "list"] as const,
  predicate: (id: number) => [...keys.predicateLists(), id] as const,
};

export const useGetPredicates = () => useQuery(keys.predicateLists(), predicateApi.getPredicates);

export const useGetPredicate = (id: number) =>
  useQuery(keys.predicate(id), () => predicateApi.getPredicate(id), { enabled: !!id, retry: false });

export const useCreatePredicate = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdlPredicateRequest) => predicateApi.postPredicate(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allPredicates),
  });
};

export const useDeletePredicate = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation(() => predicateApi.deletePredicate(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.predicateLists()),
  });
};
