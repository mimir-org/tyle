import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { RdlPurposeRequest } from "types/common/rdlPurposeRequest";
import { purposeApi } from "./purpose.api";

const keys = {
  allPurposes: ["purposes"] as const,
  purposeLists: () => [...keys.allPurposes, "list"] as const,
  purpose: (id: number) => [...keys.purposeLists(), id] as const,
};

export const useGetPurposes = () => useQuery({ queryKey: keys.purposeLists(), queryFn: purposeApi.getPurposes });

export const useGetPurpose = (id: number) =>
  useQuery({ queryKey: keys.purpose(id), queryFn: () => purposeApi.getPurpose(id), enabled: !!id, retry: false });

export const useCreatePurpose = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: RdlPurposeRequest) => purposeApi.postPurpose(item),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.allPurposes }),
  });
};

export const useDeletePurpose = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => purposeApi.deletePurpose(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.purposeLists() }),
  });
};
