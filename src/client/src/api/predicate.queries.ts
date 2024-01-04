import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { RdlPredicateRequest } from "types/attributes/rdlPredicateRequest";
import { predicateApi } from "./predicate.api";

const keys = {
  allPredicates: ["predicates"] as const,
  predicateLists: () => [...keys.allPredicates, "list"] as const,
  predicate: (id: number) => [...keys.predicateLists(), id] as const,
};

export const useGetPredicates = () =>
  useQuery({ queryKey: keys.predicateLists(), queryFn: predicateApi.getPredicates });

export const useGetPredicate = (id: number) =>
  useQuery({ queryKey: keys.predicate(id), queryFn: () => predicateApi.getPredicate(id), enabled: !!id, retry: false });

export const useCreatePredicate = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: RdlPredicateRequest) => predicateApi.postPredicate(item),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.allPredicates }),
  });
};

export const useDeletePredicate = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => predicateApi.deletePredicate(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.predicateLists() }),
  });
};
