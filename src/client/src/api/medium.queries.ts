import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { RdlMediumRequest } from "types/terminals/rdlMediumRequest";
import { mediumApi } from "./medium.api";

const keys = {
  allMedia: ["media"] as const,
  mediumLists: () => [...keys.allMedia, "list"] as const,
  medium: (id: number) => [...keys.mediumLists(), id] as const,
};

export const useGetMedia = () => useQuery({ queryKey: keys.mediumLists(), queryFn: mediumApi.getMedia });

export const useGetMedium = (id: number) =>
  useQuery({ queryKey: keys.medium(id), queryFn: () => mediumApi.getMedium(id), enabled: !!id, retry: false });

export const useCreateMedium = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (item: RdlMediumRequest) => mediumApi.postMedium(item),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.allMedia }),
  });
};

export const useDeleteMedium = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => mediumApi.deleteMedium(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: keys.mediumLists() }),
  });
};
