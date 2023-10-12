import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { classifierApi } from "./classifier.api";
import { RdlClassifierRequest } from "common/types/common/rdlClassifierRequest";

const keys = {
  allClassifiers: ["classifiers"] as const,
  classifierLists: () => [...keys.allClassifiers, "list"] as const,
  classifier: (id: number) => [...keys.classifierLists(), id] as const,
};

export const useGetClassifiers = () => useQuery(keys.classifierLists(), classifierApi.getClassifiers);

export const useGetClassifier = (id: number) =>
  useQuery(keys.classifier(id), () => classifierApi.getClassifier(id), { enabled: !!id, retry: false });

export const useCreateClassifier = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdlClassifierRequest) => classifierApi.postClassifier(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allClassifiers),
  });
};

export const useDeleteClassifier = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation(() => classifierApi.deleteClassifier(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.classifierLists()),
  });
};
