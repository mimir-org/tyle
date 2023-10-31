import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { purposeApi } from "./purpose.api";
import { RdlPurposeRequest } from "common/types/common/rdlPurposeRequest";

const keys = {
  allPurposes: ["purposes"] as const,
  purposeLists: () => [...keys.allPurposes, "list"] as const,
  purpose: (id: number) => [...keys.purposeLists(), id] as const,
};

export const useGetPurposes = () => useQuery(keys.purposeLists(), purposeApi.getPurposes);

export const useGetPurpose = (id: number) =>
  useQuery(keys.purpose(id), () => purposeApi.getPurpose(id), { enabled: !!id, retry: false });

export const useCreatePurpose = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdlPurposeRequest) => purposeApi.postPurpose(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allPurposes),
  });
};

export const useDeletePurpose = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation(() => purposeApi.deletePurpose(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.purposeLists()),
  });
};
