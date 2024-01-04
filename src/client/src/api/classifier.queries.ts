import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { RdlClassifierRequest } from "types/common/rdlClassifierRequest";
import { classifierApi } from "./classifier.api";

const keys = {
  allClassifiers: ["classifiers"] as const,
  classifierLists: () => [...keys.allClassifiers, "list"] as const,
  classifier: (id: number) => [...keys.classifierLists(), id] as const,
};

export const useGetClassifiers = () =>
  useQuery({ queryKey: keys.classifierLists(), queryFn: classifierApi.getClassifiers });

export const useGetClassifier = (id: number) =>
  useQuery({
    queryKey: keys.classifier(id),
    queryFn: () => classifierApi.getClassifier(id),
    enabled: !!id,
    retry: false,
  });

export const useCreateClassifier = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: RdlClassifierRequest) => classifierApi.postClassifier(item),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.allClassifiers }),
  });
};

export const useDeleteClassifier = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => classifierApi.deleteClassifier(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.classifierLists() }),
  });
};
